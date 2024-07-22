using System.ComponentModel.DataAnnotations.Schema;
using VoiceDecoder.Abstractions;

namespace VoiceDecoder.Entities.Intermediate;

public class ContactVoiceEntity : ObjectEntity
{
    public Guid ContactId { get; set; }

    [ForeignKey(nameof(ContactId))]
    [InverseProperty(nameof(ContactEntity.Voices))]
    public ContactEntity Contact { get; set; } = null!;

    public Guid VoiceId { get; set; }

    [ForeignKey(nameof(VoiceId))]
    [InverseProperty(nameof(VoiceEntity.Contacts))]
    public VoiceEntity Voice { get; set; } = null!;
}