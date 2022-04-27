#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GCourrier.Server.Data;
using GCourrier.Shared;
using GCourrier.Server.Models;

namespace GCourrier.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        //Added By Scaffolding Controller 
        //Connect directly to Database 
        /*private readonly GCourrierDbContext _context;

        public AgentsController(GCourrierDbContext context)
        {
            _context = context;
        }

        //----------------------------------------------

      
        // GET: api/Agents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgent()
        {
            return await _context.Agent.ToListAsync();
        }

        // GET: api/Agents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agent>> GetAgent(int id)
        {
            var agent = await _context.Agent.FindAsync(id);

            if (agent == null)
            {
                return NotFound();
            }

            return agent;
        }

        // PUT: api/Agents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgent(int id, Agent agent)
        {
            if (id != agent.Id)
            {
                return BadRequest();
            }

            _context.Entry(agent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Agents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agent>> PostAgent(Agent agent)
        {
            _context.Agent.Add(agent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgent", new { id = agent.Id }, agent);
        }

        // DELETE: api/Agents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            var agent = await _context.Agent.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }

            _context.Agent.Remove(agent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgentExists(int id)
        {
            return _context.Agent.Any(e => e.Id == id);
        }*/

        private readonly IAgentRepository agentRepository;

        public AgentsController(IAgentRepository agentRepository)
        {
            this.agentRepository = agentRepository;
        }
        //Get: Search
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Agent>>> Search(string name, Power? power)
        {
            try
            {
                var result = await agentRepository.Search(name, power);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
        //Get: api/Agents
        [HttpGet]
        public async Task<ActionResult> GetAgents()
        {
            try
            {
                return Ok(await agentRepository.GetAgents());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        //Get: api/Agents/2
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Agent>> GetAgent(int id)
        {
            try
            {
                var result = await agentRepository.GetAgent(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // POST: api/Agents
        [HttpPost]
        public async Task<ActionResult<Agent>> CreateAgent(Agent agent)
        {
            try
            {
                if (agent == null)
                    return BadRequest();

                var ag = await agentRepository.GetAgentByEmail(agent.Email);

                if (ag != null)
                {
                    ModelState.AddModelError("Email", "Agent email already in use");
                    return BadRequest(ModelState);
                }

                var createdAgent = await agentRepository.AddAgent(agent);

                return CreatedAtAction(nameof(GetAgent),
                    new { id = createdAgent.Id }, createdAgent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Agent record");
            }
        }
        // PUT: api/Agents/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Agent>> UpdateAgent(int id, Agent agent)
        {
            try
            {
                if (id != agent.Id)
                    return BadRequest("Agent ID mismatch");

                var agentToUpdate = await agentRepository.GetAgent(id);

                if (agentToUpdate == null)
                {
                    return NotFound($"Agent with Id = {id} not found");
                }

                return await agentRepository.UpdateAgent(agent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Agent record");
            }
        }
        // DELETE: api/Agents/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAgent(int id)
        {
            try
            {
                var agentToDelete = await agentRepository.GetAgent(id);

                if (agentToDelete == null)
                {
                    return NotFound($"Agent with Id = {id} not found");
                }

                await agentRepository.DeleteAgent(id);

                return Ok($"Agent with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting Agent record");
            }
        }
    }
}
