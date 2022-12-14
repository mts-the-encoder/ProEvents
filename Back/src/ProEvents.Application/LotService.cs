using AutoMapper;
using ProEvents.Application.Contracts;
using ProEvents.Application.Dto;
using ProEvents.Domain;
using ProEvents.Persistence.contracts;

namespace ProEvents.Application
{
    public class LotService : ILotService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly ILotPersist _lotPersist;
        private readonly IMapper _mapper;

        public LotService(IGeneralPersist generalPersist, 
                            ILotPersist lotPersist,
                            IMapper mapper)
        {
            _generalPersist = generalPersist;
            _lotPersist = lotPersist;
            _mapper = mapper;
        }

        private async Task AddLot(int eventId, LotDto model)
        {
            try
            {
                var lot = _mapper.Map<Lot>(model);
                lot.EventId = eventId;

                _generalPersist.Add<Lot>(lot);

                await _generalPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task UpdateLot(int eventId,LotDto model)
        {
            try
            {
                var lots = await _lotPersist.GetLotsByEventId(eventId);

                var lot = lots.FirstOrDefault(x => x.Id == model.Id);
                model.EventId = eventId;

                _mapper.Map(model,lot);

                _generalPersist.Update<Lot>(lot);

                await _generalPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Delete(int eventId, int lotId)
        {
            try
            {
                var lot = await _lotPersist.GetLotByIdAsync(eventId, lotId);

                if (lot == null) throw new Exception("Lot not found");

                _generalPersist.Delete<Lot>(lot);

                return await _generalPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LotDto[]> SaveLot(int eventId, LotDto[] models)
        {
            try
            {
                var lots = await _lotPersist.GetLotsByEventId(eventId);

                foreach (var model in models)
                {
                    switch (model.Id)
                    {
                        case 0:
                            await AddLot(eventId, model);
                            break;
                        default:
                        {
                            await UpdateLot(eventId, model);
                            break;
                        }
                    }
                }

                var lotsReturn = await _lotPersist.GetLotsByEventId(eventId);
                return _mapper.Map<LotDto[]>(lotsReturn);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LotDto[]?> GetLotsByEventIdAsync(int eventId)
        {
            try
            {
                var lots = await _lotPersist.GetLotsByEventId(eventId);

                var res = _mapper.Map<LotDto[]>(lots);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LotDto?> GetLotByIdAsync(int eventId, int lotId)
        {
            try
            {
                var lot = await _lotPersist.GetLotByIdAsync(eventId, lotId);

                var res = _mapper.Map<LotDto>(lot);

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
