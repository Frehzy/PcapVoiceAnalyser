using System.ComponentModel;

namespace VoiceDecoder.Models;

public class VoiceTrack
{
    [DisplayName("Идентификатор")]
    public Guid Id { get; }

    [Browsable(false)]
    public string FilePath { get; }

    [DisplayName("Длительность трека")]
    public TimeSpan Duration { get; set; }

    public VoiceTrack(Guid id, string filePath)
    {
        Id = id;
        FilePath = filePath;
    }
}