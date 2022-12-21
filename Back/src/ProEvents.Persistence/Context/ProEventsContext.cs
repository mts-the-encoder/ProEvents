using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Domain.Identity;

namespace ProEvents.Persistence.Context
{
    public class ProEventsContext : IdentityDbContext<User, Role, int,
                                                      IdentityUserClaim<int>, IdentityUserRole<int>, 
                                                      IdentityUserLogin<int>,
                                                      IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ProEventsContext(DbContextOptions<ProEventsContext> options)
            : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventsSpeakers { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(x => new { x.UserId, x.RoleId });

                userRole.HasOne(x => x.Role)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.RoleId)
                    .IsRequired(); 
            });

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
