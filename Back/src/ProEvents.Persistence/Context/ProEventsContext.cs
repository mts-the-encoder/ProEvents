using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;

namespace ProEvents.Persistence.Context
{
    public class ProEventsContext : DbContext
    {
        public ProEventsContext(DbContextOptions<ProEventsContext> options)
            : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Lot?> Lots { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventsSpeakers { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventSpeaker>()
                .HasKey(x => new { x.EventId, x.SpeakerId });

            modelBuilder.Entity<Event>()
                .HasMany(x => x.SocialMedias)
                .WithOne(x => x.Event)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Speaker>()
                .HasMany(x => x.SocialMedias)
                .WithOne(x => x.Speaker)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
