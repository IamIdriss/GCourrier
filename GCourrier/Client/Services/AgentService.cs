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

        public async Task<IEnumerable<Agent>> GetAllAgents()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Agent>>("/api/Agents/all");
        }

        public async Task<Agent> AddAgent(Agent Agent)
        {
            var response = await httpClient.PostAsJsonAsync<Agent>("/api/Agents", Agent);
            return await response.Content.ReadFromJsonAsync<Agent>();
        }

        public async Task DeleteAgent(int Id)
        {
            await httpClient.DeleteAsync($"/api/Agents/{Id}");
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

        public async Task<Agent> UpdateAgent(Agent agent)
        {
            var response = await httpClient
                .PutAsJsonAsync<Agent>($"/api/Agents/{agent.Id}", agent);
            return await response.Content.ReadFromJsonAsync<Agent>();
        }

        
    }
}

