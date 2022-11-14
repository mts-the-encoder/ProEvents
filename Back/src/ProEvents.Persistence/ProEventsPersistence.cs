using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Persistence
{
    public class ProEventsPersistence : IProEventsPersistence
    {
        private readonly ProEventsContext _context;
        public ProEventsPersistence(ProEventsContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers)
        {
            throw new NotImplementedException();
        }

        public Task<Event[]> GetAllEventsAsync(bool includeSpeakers)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventsByIdAsync(int eventId, bool includeSpeakers)
        {
            throw new NotImplementedException();
        }

        public Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents)
        {
            throw new NotImplementedException();
        }

        public Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents)
        {
            throw new NotImplementedException();
        }

        public Task<Speaker> GetSpeakersByIdAsync(int speakerId, bool includeEvents)
        {
            throw new NotImplementedException();
        }
    }
}
