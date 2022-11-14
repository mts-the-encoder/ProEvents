using ProEvents.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEvents.Persistence.contracts
{
    public interface IEventPersist
    {
        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers);
        Task<Event[]> GetAllEventsAsync(bool includeSpeakers);
        Task<Event> GetEventsByIdAsync(int eventId, bool includeSpeakers);
    }
}
