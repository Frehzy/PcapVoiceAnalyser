using PacketDotNet;

namespace VoiceDecoder.Models;

public class Rtp
{
    public RtpPacket Packet { get; }

    public int PacketNumber { get; }

    public UdpPacket UdpPacket => (UdpPacket)Packet.ParentPacket;

    public IPv4Packet IPv4Packet => (IPv4Packet)Packet.ParentPacket.ParentPacket;

    public uint SsrcIdentifier => Packet.SsrcIdentifier;

    public uint Timestamp => Packet.Timestamp;

    public ushort SequenceNumber => Packet.SequenceNumber;

    public Rtp(RtpPacket packet, int packetNumber)
    {
        Packet = packet;
        PacketNumber = packetNumber;
    }
}