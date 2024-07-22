using Microsoft.EntityFrameworkCore;
using VoiceDecoder.Entities;
using VoiceDecoder.Entities.Intermediate;

namespace VoiceDecoder;

public class ApplicationContext : DbContext
{
    public DbSet<ContactVoiceEntity> ContactVoiceEntities { get; set; } = null!;

    public DbSet<ContactEntity> Contacts { get; set; } = null!;

    public DbSet<VoiceEntity> Voices { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactVoiceEntity>()
            .HasOne(cv => cv.Contact)
            .WithMany(c => c.Voices)
            .HasForeignKey(cv => cv.ContactId);

        modelBuilder.Entity<ContactVoiceEntity>()
            .HasOne(cv => cv.Voice)
            .WithMany(v => v.Contacts)
            .HasForeignKey(cv => cv.VoiceId);

        base.OnModelCreating(modelBuilder);
    }
}