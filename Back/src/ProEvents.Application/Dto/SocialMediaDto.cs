using ProEvents.Domain;

namespace ProEvents.Application.Dto
{
    public class SocialMediaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? EventId { get; set; }
        public EventDto? Event { get; set; }
        public int? SpeakerId { get; set; }
        public SpeakerDto? Speaker { get; set; }
    }
}
