using GCourrier.Shared;

namespace GCourrier.Client.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<Agent>> Search(string name, Power? power);
        Task<IEnumerable<Agent>> GetAgents();
        Task<Agent> GetAgent(int AgentId);
        Task<Agent> GetAgentByEmail(string email);
        Task<Agent> AddAgent(Agent agent);
        Task<Agent> UpdateAgent(Agent agent);
        Task DeleteAgent(int Id);
    }
}
