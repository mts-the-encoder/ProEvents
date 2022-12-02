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
            return null;
            //try
            //{
            //    _generalPersist.Add<EventDto>(model);

            //    if (await _generalPersist.SaveChangesAsync())
            //        return await _eventPersist.GetEventByIdAsync(model.Id, false);

            //    return null;
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message);
            //}
        }

        public async Task<EventDto> UpdateEvent(int eventId, EventDto model)
        {
            return null;
            //try
            //{
            //    var res = await _eventPersist.GetEventByIdAsync(eventId, false);

            //    if (res == null) return null;

            //    model.Id = res.Id;

            //    _generalPersist.Update(model);

            //    if (await _generalPersist.SaveChangesAsync())
            //        return await _eventPersist.GetEventByIdAsync(model.Id, false);

            //    return null;

            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message);
            //}
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

        public async Task<EventDto> GetEventsByIdAsync(int eventId, bool includeSpeakers = false)
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
