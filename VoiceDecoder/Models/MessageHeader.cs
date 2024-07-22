using VoiceDecoder.Contacts;

namespace VoiceDecoder.Models;

public class MessageHeader : IDictionaryEntity<string>
{
    public string Key { get; }

    public string Value { get; }

    public MessageHeader(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public override string ToString() => $"{Key}:{Value}";
}