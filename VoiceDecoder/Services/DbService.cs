using Microsoft.EntityFrameworkCore;
using VoiceDecoder.Entities;

namespace VoiceDecoder.Services;

public class DbService
{
    private static DbService? _instance;
    private readonly ApplicationContext _context;

    private DbService(ApplicationContext context)
    {
        _context = context;
    }

    public static DbService? Instance => _instance;

    public static DbService GetInstance(ApplicationContext context) => _instance ??= new DbService(context);

    public void AddContact(ContactEntity contact)
    {
        _context.Contacts.Add(contact);
        _context.SaveChanges();
    }

    public void DeleteContact(int contactId)
    {
        var contact = _context.Contacts.Find(contactId);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }
    }

    public void UpdateContact(ContactEntity contact)
    {
        _context.Entry(contact).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void AddVoice(VoiceEntity voice)
    {
        _context.Voices.Add(voice);
        _context.SaveChanges();
    }

    public void DeleteVoice(int voiceId)
    {
        var voice = _context.Voices.Find(voiceId);
        if (voice != null)
        {
            _context.Voices.Remove(voice);
            _context.SaveChanges();
        }
    }

    public void UpdateVoice(VoiceEntity voice)
    {
        _context.Entry(voice).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<ContactEntity> GetAllContacts()
    {
        return _context.Contacts.ToList();
    }

    public ContactEntity GetContactById(int contactId)
    {
        return _context.Contacts.Find(contactId);
    }

    public List<VoiceEntity> GetAllVoices()
    {
        return _context.Voices.ToList();
    }

    public VoiceEntity GetVoiceById(int voiceId)
    {
        return _context.Voices.Find(voiceId);
    }
}