using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.contracts;

namespace ProEvents.Persistence
{
    public class SpeakerPersistence : ISpeakerPersist
    {
        private readonly ProEventsContext _context;

        public SpeakerPersistence(ProEventsContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name,bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(x => x.SocialMedias);

            if (includeEvents)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Event);

            query = query.AsNoTracking().OrderBy(x => x.Id)
                .Where(x => x.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(x => x.SocialMedias);

            if (includeEvents)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Event);

            query = query.AsNoTracking().OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Speaker> GetSpeakersByIdAsync(int speakerId,bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(x => x.SocialMedias);

            if (includeEvents)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Event);

            query = query.AsNoTracking().OrderBy(x => x.Id)
                .Where(x => x.Id == speakerId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
