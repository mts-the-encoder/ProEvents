using ProEvents.Domain;

namespace ProEvents.Persistence.Contracts
{
    public interface IEventPersist
    {
        Task<Event[]> GetAllEventsByThemeAsync(int userId, string theme, bool includeSpeakers = false);
        Task<Event[]> GetAllEventsAsync(int userId, bool includeSpeakers = false);
        Task<Event> GetEventByIdAsync(int userId, int eventId, bool includeSpeakers = false);
    }
}
