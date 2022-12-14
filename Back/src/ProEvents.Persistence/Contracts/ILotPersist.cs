using ProEvents.Domain;

namespace ProEvents.Persistence.contracts
{
    public interface ILotPersist
    {
        Task<Lot?[]> GetLotsByEventId(int eventId);
        Task<Lot?> GetLotByIdAsync(int eventId, int id);
    }
}
