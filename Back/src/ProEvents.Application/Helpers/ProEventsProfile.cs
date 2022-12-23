using AutoMapper;
using ProEvents.Application.Dto;
using ProEvents.Domain;
using ProEvents.Domain.Identity;

namespace ProEvents.Application.Helpers
{
    public class ProEventsProfile : Profile
    {
        public ProEventsProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Lot, LotDto>().ReverseMap();
            CreateMap<SocialMedia, SocialMediaDto>().ReverseMap();
            CreateMap<Speaker, SpeakerDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}
