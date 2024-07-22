using System.Text.RegularExpressions;

namespace VoiceDecoder.Models;

public class Contact
{
    public string PhoneNumber { get; } = string.Empty;

    public string Ip { get; } = string.Empty;

    public string Tag { get; } = string.Empty;

    public Contact(string data)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(data);

        var parts = data.Split(';');

        var sipMatch = Regex.Match(parts[0], "<sip:(.*?)>");
        if (sipMatch.Success)
        {
            var sipPart = sipMatch.Groups[1].Value;

            if (sipPart.Contains('@'))
            {
                PhoneNumber = sipPart.Split('@')[0];
                Ip = sipPart.Split('@')[1].Split(':')[0];
            }
            else
                Ip = sipPart.Split(':')[0];
        }

        foreach (var part in parts.Where(part => part.StartsWith("tag=")))
            Tag = part[4..];
    }

    public override string ToString()
    {
        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(PhoneNumber))
            parts.Add(PhoneNumber);

        if (!string.IsNullOrWhiteSpace(Ip))
            parts.Add(Ip);

        if (!string.IsNullOrWhiteSpace(Tag))
            parts.Add(Tag);

        return string.Join(" ", parts);
    }
}