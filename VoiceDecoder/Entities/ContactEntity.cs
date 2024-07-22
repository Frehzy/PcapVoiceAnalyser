using System.ComponentModel.DataAnnotations.Schema;
using VoiceDecoder.Abstractions;
using VoiceDecoder.Entities.Intermediate;

namespace VoiceDecoder.Entities;

public class ContactEntity : ObjectEntity
{
    public string Ip { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    [InverseProperty(nameof(ContactVoiceEntity.Contact))]
    public ICollection<ContactVoiceEntity> Voices { get; set; } = [];
}