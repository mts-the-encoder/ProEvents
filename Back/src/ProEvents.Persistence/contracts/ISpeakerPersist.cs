using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Persistence.contracts
{
    public interface ISpeakerPersist
    {
        Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents);
        Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents);
        Task<Speaker> GetSpeakersByIdAsync(int speakerId, bool includeEvents);
    }
}
