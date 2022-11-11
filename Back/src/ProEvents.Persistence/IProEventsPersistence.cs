using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Persistence
{
    public interface IProEventsPersistence
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers);
        Task<Event[]> GetAllEventsAsync(bool includeSpeakers);
        Task<Event> GetEventsByIdAsync(int eventId, bool includeSpeakers);

        Task<Speaker[]> GetAllSpeakersByNameAsync(string name,bool includeEvents);
        Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents);
        Task<Speaker> GetSpeakersByIdAsync(int speakerId, bool includeEvents);
    }
}
