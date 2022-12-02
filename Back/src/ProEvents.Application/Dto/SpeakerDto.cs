using ProEvents.Domain;

namespace ProEvents.Application.Dto
{
    public class SpeakerDto
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Curriculum{ get; set; }
        public string ImageURL{ get; set; }
        public string Phone{ get; set; }
        public string Email{ get; set; }
        public IEnumerable<SocialMediaDto> SocialMedias{ get; set; }
        public IEnumerable<SpeakerDto> EventsSpeakers{ get; set; }
    }
}
