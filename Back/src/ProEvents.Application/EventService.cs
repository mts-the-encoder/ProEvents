using ProEvents.Application.Contracts;
using ProEvents.Domain;
using ProEvents.Persistence.contracts;

namespace ProEvents.Application
{
    public class EventService : IEventService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly IEventPersist _eventPersist;

        public EventService(IGeneralPersist generalPersist, IEventPersist eventPersist)
        {
            _generalPersist = generalPersist;
            _eventPersist = eventPersist;
        }

        public async Task<Event> AddEvents(Event model)
        {
            try
            {
                _generalPersist.Add(model);

                if (await _generalPersist.SaveChangesAsync())
                    return await _eventPersist.GetEventByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Event> UpdateEvent(int eventId, Event model)
        {
            try
            {
                var res = await _eventPersist.GetEventByIdAsync(eventId, false);

                if (res == null) return null;

                model.Id = res.Id;

                _generalPersist.Update(model);

                if (await _generalPersist.SaveChangesAsync())
                    return await _eventPersist.GetEventByIdAsync(model.Id,false);

                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            try
            {
                var res = await _eventPersist.GetEventByIdAsync(eventId,false);

                if (res == null) throw new Exception("Event not found");

                _generalPersist.Delete<Event>(res);

                return await _generalPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }

        public Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventsByIdAsync(int eventId, bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }
    }
}
