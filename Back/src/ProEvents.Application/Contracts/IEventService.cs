using ProEvents.Application.Dto;

namespace ProEvents.Application.Contracts
{
    public interface IEventService
    {
        Task<EventDto> AddEvents(int userId, EventDto model);
        Task<EventDto> UpdateEvent(int userId, int eventId, EventDto model);
        Task<bool> DeleteEvent(int userId, int eventId);

        Task<EventDto[]> GetAllEventsAsync(int userId, bool includeSpeakers = false);
        Task<EventDto[]> GetAllEventsByThemeAsync(int userId, string theme, bool includeSpeakers = false);
        Task<EventDto> GetEventByIdAsync(int userId, int eventId ,bool includeSpeakers = false);
    }
}
