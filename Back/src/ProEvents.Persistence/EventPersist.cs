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

        public async Task<Event[]> GetAllEventsByThemeAsync(int userId, string theme, bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(x => x.Lots)
                .Include(x => x.SocialMedias);

            if (includeSpeakers)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Speaker);

            query = query.AsNoTracking()
                .OrderBy(x => x.Id)
                .Where(x => x.Theme.ToLower().Contains(theme.ToLower()) && userId == x.UserId);

            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetAllEventsAsync(int userId, bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(x => x.Lots)
                .Include(x => x.SocialMedias);

            if (includeSpeakers)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Speaker);

            query = query.AsNoTracking()
                .Where(x => userId == x.UserId)
                .OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int userId, int eventId,bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(x => x.Lots)
                .Include(x => x.SocialMedias);

            if (includeSpeakers)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Speaker);

            query = query.AsNoTracking().OrderBy(x => x.Id).Where(x => x.Id == eventId && userId == x.UserId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
