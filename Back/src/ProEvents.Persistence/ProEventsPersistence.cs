using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.contracts;

namespace ProEvents.Persistence
{
    public class ProEventsPersistence : ISpeakerPersist
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

        public async Task<Event> GetEventsByIdAsync(int eventId,bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(x => x.Lots)
                .Include(x => x.SocialMedias);

            if (includeSpeakers)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Speaker);

            query = query.OrderBy(x => x.Id).Where(x => x.Id == eventId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name,bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(x => x.SocialMedias);

            if (includeEvents)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Event);

            query = query.OrderBy(x => x.Id)
                .Where(x => x.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(x => x.SocialMedias);

            if (includeEvents)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Event);

            query = query.OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Speaker> GetSpeakersByIdAsync(int speakerId,bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(x => x.SocialMedias);

            if (includeEvents)
                query = query.Include(x => x.EventsSpeakers).ThenInclude(x => x.Event);

            query = query.OrderBy(x => x.Id)
                .Where(x => x.Id == speakerId);

            return await query.FirstOrDefaultAsync();
        }
    }

}

