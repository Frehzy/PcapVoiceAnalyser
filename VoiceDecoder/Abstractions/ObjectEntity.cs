using System.ComponentModel.DataAnnotations;
using VoiceDecoder.Contacts;

namespace VoiceDecoder.Abstractions;

public class ObjectEntity : IEntity
{
    [Key]
    public Guid Identificator { get; set; } = Guid.NewGuid();
}