using ProEvents.Domain;

namespace ProEvents.Persistence.Contracts
{
    public interface ISpeakerPersist
    {
        Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents = false);
        Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false);
        Task<Speaker> GetSpeakersByIdAsync(int speakerId, bool includeEvents = false);
    }
}
