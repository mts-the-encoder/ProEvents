using ProEvents.Domain.Identity;

namespace ProEvents.Domain
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Curriculum { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<SocialMedia> SocialMedias { get; set; }
        public IEnumerable<EventSpeaker> EventsSpeakers { get; set; }
    }
}
