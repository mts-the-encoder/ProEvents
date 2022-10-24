namespace ProEvents.API.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Place { get; set; }
        public string EventDate { get; set; }
        public string Theme { get; set; }
        public int QtdPeoples { get; set; }
        public string Lot { get; set; }
        public string ImageURL { get; set; }
    }
}
