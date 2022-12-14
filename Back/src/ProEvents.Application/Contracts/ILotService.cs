using ProEvents.Application.Dto;

namespace ProEvents.Application.Contracts
{
    public interface ILotService
    {
        Task<LotDto[]> SaveLot(int eventId, LotDto[] models);
        Task<bool> Delete(int eventId, int lotId);
        Task<LotDto[]?> GetLotsByEventIdAsync(int eventId);
        Task<LotDto?> GetLotByIdAsync(int eventId, int lotId);
    }
}
