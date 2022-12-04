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

        [Range(100,120000, ErrorMessage = "must be at range 100 and 120.000")]
        public int QtdPeople { get; set; }
        public string ImageURL { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Phone(ErrorMessage = ("Please enter a valid phone number"))]
        public string Phone { get; set; }

        [Required, Display(Name = "E-mail")]  //Only for learn
        [EmailAddress(ErrorMessage = "Field {0} is not a valid email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter a valid e-mail address")]  //Only 4fun
        public string Email { get; set; }

        public IEnumerable<LotDto> Lots { get; set; }
        public IEnumerable<SocialMediaDto> SocialMedias { get; set; }
        public IEnumerable<SpeakerDto> EventsSpeakers { get; set; }
    }
}
