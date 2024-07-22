using NAudio.Wave;
using PacketDotNet;
using PacketDotNet.Utils;
using SharpPcap;
using SharpPcap.LibPcap;
using System.Text;
using VoiceDecoder.Models;

namespace VoiceDecoder.Services;

public static class PacketService
{
    public static IEnumerable<Channel> Analyse(string pcapFile)
    {
        var channels = new Dictionary<Guid, Channel>();
        var packetIndex = 0;

        using (var device = new CaptureFileReaderDevice(pcapFile))
        {
            device.Open();
            device.OnPacketArrival += (_, e) =>
            {
                packetIndex++;
                var rawCapture = e.GetPacket();
                var packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
                if (packet is EthernetPacket ethPacket && ethPacket.PayloadPacket is IPv4Packet ipPacket && ipPacket.Protocol == ProtocolType.Udp)
                {
                    var raw = Encoding.ASCII.GetString(rawCapture.Data);
                    var enumerable = ConvertToDictionary(raw);
                    var callidObj = enumerable.FirstOrDefault(x => x.Key.Equals("Call-ID"));
                    if (callidObj is not null && Guid.TryParse(callidObj.Value, out var callId))
                    {
                        var sip = new Sip(enumerable.ToList(), raw, packetIndex, ipPacket);
                        if (channels.TryGetValue(callId, out var channel))
                            channel.Sip.Add(sip);
                        else
                        {
                            channel = new Channel();
                            channel.Sip.Add(sip);
                            channels.Add(callId, channel);
                        }
                        return;
                    }

                    if (ethPacket.PayloadPacket.PayloadPacket is not null)
                    {
                        try
                        {
                            if (ethPacket.PayloadPacket.PayloadPacket.PayloadData.Length != 172)
                                return;

                            var segment = new ByteArraySegment(ethPacket.PayloadPacket.PayloadPacket.PayloadData);
                            var rtpPacket = new RtpPacket(segment, ethPacket.PayloadPacket.PayloadPacket);
                            var rtp = new Rtp(rtpPacket, packetIndex);
                            AddRTPPacket(channels, rtp);
                        }
                        catch { }
                    }

                    static IEnumerable<MessageHeader> ConvertToDictionary(string raw)
                    {
                        var lines = raw.Split(["\r\n"], StringSplitOptions.RemoveEmptyEntries);
                        var sipMessage = new List<MessageHeader>();
                        foreach (var line in lines)
                        {
                            var parts = line.Split([':'], 2);
                            if (parts.Length == 2)
                            {
                                var key = parts[0].Trim();
                                var value = parts[1].Trim();

                                yield return new MessageHeader(key, value);
                            }
                        }
                    }

                    static void AddRTPPacket(Dictionary<Guid, Channel> channels, Rtp rtpPacket)
                    {
                        if (channels.Count == 0)
                            return;

                        var channel = channels.Values.FirstOrDefault(x => x.Rtp.Keys.Any(y => y.Equals(rtpPacket.SsrcIdentifier)));
                        if (channel is not null)
                        {
                            var lastSip = channel.Sip.Last();
                            if ((lastSip.CSeq ?? string.Empty).Contains("BYE"))
                                return;

                            var rtpChannel = channel.Rtp.First(x => x.Key.Equals(rtpPacket.SsrcIdentifier)).Value;
                            rtpChannel.Add(rtpPacket);
                        }
                        else
                        {
                            channel = channels.Values.MaxBy(x => x.Sip.Last().PacketIndex)!;
                            if (channel.Sip.Any(x => (x.CSeq ?? string.Empty).Contains("BYE")))
                                return;

                            channel.Rtp.Add(rtpPacket.SsrcIdentifier, [rtpPacket]);
                        }
                    }
                }
            };
            device.Capture();
        }

        return channels.Values.Where(x => x.Rtp.Count > 0);
    }
}