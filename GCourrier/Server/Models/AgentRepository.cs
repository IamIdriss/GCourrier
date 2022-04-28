using GCourrier.Server.Data;
using GCourrier.Shared;
using Microsoft.EntityFrameworkCore;

namespace GCourrier.Server.Models
{


    public class AgentRepository : IAgentRepository
    {
        private readonly GCourrierDbContext gCourrierDbContext;
        private readonly IDepartmentRepository departmentRepository;

        public AgentRepository(GCourrierDbContext gCourrierDbContext, IDepartmentRepository departmentRepository)
        {
            this.gCourrierDbContext = gCourrierDbContext;
            this.departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Agent>> GetAllAgent()
        {
            return await gCourrierDbContext.Agent.Include(a => a.Department).ToListAsync();
        }

        public async Task<Agent> AddAgent(Agent agent)
        {
            if (agent.DepartmentId == 0)
            {
                throw new Exception("Agent DepartmentId cannot be ZERO");
            }
            else
            {
                Department department = await this.departmentRepository
                    .GetDepartment(agent.DepartmentId);
                if (department == null)
                {
                    throw new Exception($"Invalid Agent DepartmentId {agent.DepartmentId}");
                }
                agent.Department = department;
            }

            var result = await gCourrierDbContext.Agent.AddAsync(agent);
            await gCourrierDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAgent(int Id)
        {
            var result = await gCourrierDbContext.Agent
                .FirstOrDefaultAsync(a => a.Id == Id);

            if (result != null)
            {
                gCourrierDbContext.Agent.Remove(result);
                await gCourrierDbContext.SaveChangesAsync();
            }
        }

       

        public async Task<Agent> GetAgentByEmail(string email)
        {
            return await gCourrierDbContext.Agent
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        
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

        public async Task<Agent> UpdateAgent(Agent agent)
        {
            var result = await gCourrierDbContext.Agent
                .FirstOrDefaultAsync(a => a.Id == agent.Id);

            if (result != null)
            {
                result.FirstName = agent.FirstName;
                result.LastName = agent.LastName;
                result.Email = agent.Email;

                result.Power = agent.Power;
                if (agent.DepartmentId != 0)
                {
                    result.DepartmentId = agent.DepartmentId;
                }
                else if (agent.Department != null)
                {
                    result.DepartmentId = agent.Department.Id;
                }
                result.PhotoPath = agent.PhotoPath;

                await gCourrierDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
    

