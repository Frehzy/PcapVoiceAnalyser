using VoiceDecoder.Contacts;

namespace VoiceDecoder.Extentions;

public static class ListExtention
{
    public static T? GetValueOrDefault<T, U>(this List<T> data, string key) where T : IDictionaryEntity<U>
    {
        var obj = data.FirstOrDefault(x => x.Key.Equals(key));
        if (obj is null)
            return default;
        else
        {
            data.Remove(obj);
            return obj;
        }
    }
}