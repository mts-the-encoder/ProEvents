using AutoMapper;
using ProEvents.Application.Dto;
using ProEvents.Domain;

namespace ProEvents.Application.Helpers
{
    public class ProEventsProfile : Profile
    {
        public ProEventsProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}
