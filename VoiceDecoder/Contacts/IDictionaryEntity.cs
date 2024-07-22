namespace VoiceDecoder.Contacts;

public interface IDictionaryEntity<T>
{
    public string Key { get; }

    public T Value { get; }
}