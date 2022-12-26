using AutoMapper;
using ProEvents.Application.Contracts;
using ProEvents.Application.Dto;
using ProEvents.Domain;
using ProEvents.Persistence.Contracts;

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

        public async Task<EventDto> AddEvents(int userId, EventDto model)
        {
            try
            {
                var eventDomain = _mapper.Map<Event>(model);
                eventDomain.UserId = userId;

                _generalPersist.Add<Event>(eventDomain);

                if (!await _generalPersist.SaveChangesAsync()) return null;
                
                var res = await _eventPersist.GetEventByIdAsync(userId, eventDomain.Id, false);
                return _mapper.Map<EventDto>(res);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto> UpdateEvent(int userId, int eventId, EventDto model)
        {
            try
            {
                var eventDomain = await _eventPersist.GetEventByIdAsync(userId, eventId, false);

                if (eventDomain == null) return null;

                model.Id = eventDomain.Id;
                userId = model.UserId;

                _mapper.Map(model, eventDomain);

                _generalPersist.Update<Event>(eventDomain);

                if (!await _generalPersist.SaveChangesAsync()) return null;

                var res = await _eventPersist.GetEventByIdAsync(userId, eventDomain.Id,false);
                return _mapper.Map<EventDto>(res);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEvent(int userId, int eventId)
        {
            try
            {
                var res = await _eventPersist.GetEventByIdAsync(userId, eventId,false);

                if (res == null) throw new Exception("Event not found");

                _generalPersist.Delete<Event>(res);

                return await _generalPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto[]> GetAllEventsAsync(int userId, bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsAsync(userId, includeSpeakers);

                var res = _mapper.Map<EventDto[]>(events);

                return res == null ? null : res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto[]> GetAllEventsByThemeAsync(int userId, string theme, bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsByThemeAsync(userId, theme, includeSpeakers);

                var res = _mapper.Map<EventDto[]>(events);

                return res == null ? null : res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto> GetEventByIdAsync(int userId, int eventId, bool includeSpeakers = false)
        {
            try
            {
                var eventById = await _eventPersist.GetEventByIdAsync(userId, eventId, includeSpeakers);

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
