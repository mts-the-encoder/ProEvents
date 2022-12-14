using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.contracts;

namespace ProEvents.Persistence
{
    public class LotPersist : ILotPersist
    {
        private readonly ProEventsContext _context;

        public LotPersist(ProEventsContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public async Task<Lot?[]> GetLotsByEventId(int eventId)
        {
            IQueryable<Lot?> query = _context.Lots;

            query = query.AsNoTracking()
                .Where(x => x.EventId == eventId);

            return await query.ToArrayAsync();
        }

        public async Task<Lot?> GetLotByIdAsync(int eventId, int id)
        {
            IQueryable<Lot?> query = _context.Lots;

            query = query
                    .Where(x => x.EventId == eventId
                        && x.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
