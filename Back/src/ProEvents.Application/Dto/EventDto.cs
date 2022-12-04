using System.ComponentModel.DataAnnotations;

namespace ProEvents.Application.Dto
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? EventDate { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters"), 
         MaxLength(50, ErrorMessage = "Is longer than the 50 characters")]
        public string Theme { get; set; }


        public int QtdPeople { get; set; }
        public string ImageURL { get; set; }
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Field {0} is not a valid email")]
        [Required]
        public string Email { get; set; }

        public IEnumerable<LotDto> Lots { get; set; }
        public IEnumerable<SocialMediaDto> SocialMedias { get; set; }
        public IEnumerable<SpeakerDto> EventsSpeakers { get; set; }
    }
}
