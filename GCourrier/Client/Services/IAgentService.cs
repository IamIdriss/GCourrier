using GCourrier.Shared;

namespace GCourrier.Client.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<Agent>> Search(string name, Power? power);
        /*Task<IEnumerable<Agent>> GetAllAgents(int skip, int take, string orderBy);*/
        Task<IEnumerable<Agent>> GetAllAgents();
        Task<Agent> GetAgent(int AgentId);
        Task<Agent> GetAgentByEmail(string email);
        Task<Agent> AddAgent(Agent agent);
        Task<Agent> UpdateAgent(Agent agent);
        Task DeleteAgent(int Id);
    }
}
