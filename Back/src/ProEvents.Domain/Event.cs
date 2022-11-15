namespace ProEvents.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? EventDate { get; set; }
        public string Theme { get; set; }
        public int QtdPeoples { get; set; }
        public string ImageURL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Lot>? Lots { get; set; }
        public IEnumerable<SocialMedia>? SocialMedias { get; set; }
        public IEnumerable<EventSpeaker>? EventsSpeakers { get; set; }
    }
}
