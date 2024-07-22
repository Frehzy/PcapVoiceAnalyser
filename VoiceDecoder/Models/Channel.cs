using VoiceDecoder.Decoders;

namespace VoiceDecoder.Models;

public class Channel
{
    public List<Sip> Sip { get; } = [];

    public Dictionary<uint, List<Rtp>> Rtp { get; } = [];

    public Guid CallId => Sip.First().CallId;

    public Contact From => Sip.First().From!;

    public Contact To => Sip.First().To!;

    public byte[] GetVoice()
    {
        if (Rtp.Count == 0)
            return Enumerable.Empty<byte>().ToArray();

        var mainRtp = Rtp.Values.MaxBy(x => x.Count)!;
        return mainRtp.Select(x => x.Packet.PayloadData).Aggregate(Array.Empty<byte>(), (acc, next) => acc.Concat(next).ToArray());
    }

    public byte[] GetDecodeVoice()
    {
        var voice = GetVoice();
        if (voice.Length == 0)
            return voice;

        var decodeVoice = ALawDecoder.Decode(voice);

        var bytes = new byte[decodeVoice.Length * 2];
        Buffer.BlockCopy(decodeVoice, 0, bytes, 0, bytes.Length);

        return bytes;
    }
}