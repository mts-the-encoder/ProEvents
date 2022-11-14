using ProEvents.Domain;

namespace ProEvents.Persistence.contracts
{
    public interface IEventPersist
    {
        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false);
        Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false);
        Task<Event> GetEventsByIdAsync(int eventId, bool includeSpeakers = false);
    }
}
