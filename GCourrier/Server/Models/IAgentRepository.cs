using GCourrier.Shared;

namespace GCourrier.Server.Models
{
    public interface IAgentRepository
    {
        Task<IEnumerable<Agent>> Search(string name, Power? power);
        Task<IEnumerable<Agent>> GetAgents();
        Task<Agent> GetAgent(int Id);
        Task<Agent> GetAgentByEmail(string email);
        Task<Agent> AddAgent(Agent Agent);
        Task<Agent> UpdateAgent(Agent Agent);
        Task DeleteAgent(int Id);
    }
}
