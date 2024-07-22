using System.ComponentModel.DataAnnotations.Schema;
using VoiceDecoder.Abstractions;
using VoiceDecoder.Entities.Intermediate;

namespace VoiceDecoder.Entities;

public class VoiceEntity : ObjectEntity
{
    public required Guid CallId { get; set; }

    public required string VoicePath { get; set; }

    [InverseProperty(nameof(ContactVoiceEntity.Voice))]
    public ICollection<ContactVoiceEntity> Contacts { get; set; } = [];
}