using GCourrier.Shared;
using System.Net.Http.Json;

namespace GCourrier.Client.Services
{
    public class AgentService : IAgentService
    {
        private readonly HttpClient httpClient;

        public AgentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Agent>> GetAgents()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Agent>>("api/Agents");
        }

        public Task<Agent> AddAgent(Agent agent)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAgent(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Agent> GetAgent(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Agent> GetAgentByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Agent>> Search(string name, Power? power)
        {
            throw new NotImplementedException();
        }

        public Task<Agent> UpdateAgent(Agent agent)
        {
            throw new NotImplementedException();
        }
    }
}

