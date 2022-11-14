using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.contracts;

namespace ProEvents.Persistence
{
    public class EventPersistence : IEventPersist
    {
        private readonly ProEventsContext _context;

        public EventPersistence(ProEventsContext context)
        {
            _context = context;
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme,bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(x => x.Lots)
                .Include(x => x.SocialMedias);

            if (includeSpeakers)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Speaker);

            query = query.OrderBy(x => x.Id)
                .Where(x => x.Theme.ToLower().Contains(theme.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(x => x.Lots)
                .Include(x => x.SocialMedias);

            if (includeSpeakers)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Speaker);

            query = query.OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId,bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(x => x.Lots)
                .Include(x => x.SocialMedias);

            if (includeSpeakers)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Speaker);

            query = query.OrderBy(x => x.Id).Where(x => x.Id == eventId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
