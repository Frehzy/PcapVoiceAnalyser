using VoiceDecoder.Extentions;
using PacketDotNet;

namespace VoiceDecoder.Models;

public class Sip
{
    public string? Via { get; }
    public Contact? From { get; }
    public Contact? To { get; }
    public string? Contact { get; }
    public Guid CallId { get; }
    public string? CSeq { get; }
    public string? Allow { get; }
    public string? Supported { get; }
    public string? SessionExpires { get; }
    public string? MinSE { get; }
    public string? PAssertedIdentity { get; }
    public string? MaxForwards { get; }
    public string? UserAgent { get; }
    public string? ContentType { get; }
    public string? ContentLength { get; }
    public List<MessageHeader> Sdp { get; }
    public string Raw { get; }
    public int PacketIndex { get; }
    public IPv4Packet Packet { get; }

    public Sip(List<MessageHeader> data, string raw, int packetIndex, IPv4Packet packet)
    {
        Via = data.GetValueOrDefault<MessageHeader, string>("Via")?.Value;
        From = new Contact(data.GetValueOrDefault<MessageHeader, string>("From")!.Value);
        To = new Contact(data.GetValueOrDefault<MessageHeader, string>("To")!.Value);
        Contact = data.GetValueOrDefault<MessageHeader, string>("Contact")?.Value;
        CallId = Guid.Parse(data.GetValueOrDefault<MessageHeader, string>("Call-ID")!.Value);
        CSeq = data.GetValueOrDefault<MessageHeader, string>("CSeq")?.Value;
        Allow = data.GetValueOrDefault<MessageHeader, string>("Allow")?.Value;
        Supported = data.GetValueOrDefault<MessageHeader, string>("Supported")?.Value;
        SessionExpires = data.GetValueOrDefault<MessageHeader, string>("Session-Expires")?.Value;
        MinSE = data.GetValueOrDefault<MessageHeader, string>("Min-SE")?.Value;
        PAssertedIdentity = data.GetValueOrDefault<MessageHeader, string>("P-Asserted-Identity")?.Value;
        MaxForwards = data.GetValueOrDefault<MessageHeader, string>("Max-Forwards")?.Value;
        UserAgent = data.GetValueOrDefault<MessageHeader, string>("User-Agent")?.Value;
        ContentType = data.GetValueOrDefault<MessageHeader, string>("Content-Type")?.Value;
        ContentLength = data.GetValueOrDefault<MessageHeader, string>("Content-Length")?.Value;
        Sdp = data;
        Raw = raw;
        PacketIndex = packetIndex;
        Packet = packet;
    }
}