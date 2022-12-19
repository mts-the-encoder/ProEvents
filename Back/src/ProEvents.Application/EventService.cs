using AutoMapper;
using Microsoft.Extensions.Logging;
using ProEvents.Application.Contracts;
using ProEvents.Application.Dto;
using ProEvents.Domain;
using ProEvents.Persistence.contracts;

namespace ProEvents.Application
{
    public class EventService : IEventService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly IEventPersist _eventPersist;
        private readonly IMapper _mapper;

        public EventService(IGeneralPersist generalPersist, 
                            IEventPersist eventPersist,
                            IMapper mapper)
        {
            _generalPersist = generalPersist;
            _eventPersist = eventPersist;
            _mapper = mapper;
        }

        public async Task<EventDto> AddEvents(EventDto model)
        {
            try
            {
                var eventDomain = _mapper.Map<Event>(model);

                _generalPersist.Add<Event>(eventDomain);

                if (!await _generalPersist.SaveChangesAsync()) return null;
                
                var res = await _eventPersist.GetEventByIdAsync(eventDomain.Id, false);
                return _mapper.Map<EventDto>(res);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto> UpdateEvent(int eventId, EventDto model)
        {
            try
            {
                var eventDomain = await _eventPersist.GetEventByIdAsync(eventId, false);

                if (eventDomain == null) return null;

                model.Id = eventDomain.Id;

                _mapper.Map(model, eventDomain);

                _generalPersist.Update<Event>(eventDomain);

                if (!await _generalPersist.SaveChangesAsync()) return null;

                var res = await _eventPersist.GetEventByIdAsync(eventDomain.Id,false);
                return _mapper.Map<EventDto>(res);

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

        public async Task<EventDto[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsAsync(includeSpeakers);

                var res = _mapper.Map<EventDto[]>(events);

                return res == null ? null : res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsByThemeAsync(theme, includeSpeakers);

                var res = _mapper.Map<EventDto[]>(events);

                return res == null ? null : res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            try
            {
                var eventById = await _eventPersist.GetEventByIdAsync(eventId, includeSpeakers);

                var res = _mapper.Map<EventDto>(eventById);

                return res == null ? null : res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
