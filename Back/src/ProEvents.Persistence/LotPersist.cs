using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.Contracts;

namespace ProEvents.Persistence
{
    public class EventPersist : IEventPersist
    {
        private readonly ProEventsContext _context;

        public EventPersist(ProEventsContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(x => x.Lots)
                .Include(x => x.SocialMedias);

            if (includeSpeakers)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Speaker);

            query = query.AsNoTracking().OrderBy(x => x.Id)
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

            query = query.AsNoTracking().OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId,bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(x => x.Lots)
                .Include(x => x.SocialMedias);

            if (includeSpeakers)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Speaker);

            query = query.AsNoTracking().OrderBy(x => x.Id).Where(x => x.Id == eventId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
