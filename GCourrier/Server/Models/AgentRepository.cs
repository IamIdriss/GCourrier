using GCourrier.Server.Data;
using GCourrier.Shared;
using Microsoft.EntityFrameworkCore;

namespace GCourrier.Server.Models
{
   
    
        public class AgentRepository : IAgentRepository
        {
            private readonly GCourrierDbContext gCourrierDbContext;

            public AgentRepository(GCourrierDbContext gCourrierDbContext)
            {
                this.gCourrierDbContext = gCourrierDbContext;
            }

            public async Task<Agent> AddAgent(Agent Agent)
            {
                if (Agent.Department != null)
                {
                    gCourrierDbContext.Entry(Agent.Department).State = EntityState.Unchanged;
                }

                var result = await gCourrierDbContext.Agent.AddAsync(Agent);
                await gCourrierDbContext.SaveChangesAsync();
                return result.Entity;
            }

            public async Task DeleteAgent(int AgentId)
            {
                var result = await gCourrierDbContext.Agent
                    .FirstOrDefaultAsync(a => a.Id == AgentId);

                if (result != null)
                {
                    gCourrierDbContext.Agent.Remove(result);
                    await gCourrierDbContext.SaveChangesAsync();
                }
            }

            public async Task<Agent> GetAgent(int AgentId)
            {
                return await gCourrierDbContext.Agent
                    .Include(a => a.Department)
                    .FirstOrDefaultAsync(a => a.Id == AgentId);
            }

            public async Task<Agent> GetAgentByEmail(string email)
            {
                return await gCourrierDbContext.Agent
                    .FirstOrDefaultAsync(a => a.Email == email);
            }

            public async Task<IEnumerable<Agent>> GetAgents()
            {
                return await gCourrierDbContext.Agent.ToListAsync();
            }

            public async Task<IEnumerable<Agent>> Search(string name, Power? power)
            {
                IQueryable<Agent> query = gCourrierDbContext.Agent;

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(a => a.FirstName.Contains(name)
                                || a.LastName.Contains(name));
                }

                if (power != null)
                {
                    query = query.Where(a => a.Power == power);
                }

                return await query.ToListAsync();
            }

            public async Task<Agent> UpdateAgent(Agent Agent)
            {
                var result = await gCourrierDbContext.Agent
                    .FirstOrDefaultAsync(a => a.Id == Agent.Id);

                if (result != null)
                {
                    result.FirstName = Agent.FirstName;
                    result.LastName = Agent.LastName;
                    result.Email = Agent.Email;

                    result.Power = Agent.Power;
                    if (Agent.DepartmentId != 0)
                    {
                        result.DepartmentId = Agent.DepartmentId;
                    }
                    else if (Agent.Department != null)
                    {
                        result.DepartmentId = Agent.Department.Id;
                    }
                    result.PhotoPath = Agent.PhotoPath;

                    await gCourrierDbContext.SaveChangesAsync();

                    return result;
                }

                return null;
            }
        }
    }

