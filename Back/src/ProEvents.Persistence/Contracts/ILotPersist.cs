using ProEvents.Domain;

namespace ProEvents.Persistence.Contracts
{
    public interface ILotPersist
    {
        Task<Lot?[]> GetLotsByEventId(int eventId);
        Task<Lot?> GetLotByIdAsync(int eventId, int id);
    }
}
