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
        

        
            private readonly IAgentRepository agentRepository;

            public AgentsController(IAgentRepository agentRepository)
            {
                this.agentRepository = agentRepository;
            }

            [HttpGet("all")]
        public async Task<ActionResult> GetAgentAll()
        {
            try
            {
                return Ok(await agentRepository.GetAllAgent());
            }
            catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
                }
            }

            [HttpGet("search")]
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

            [HttpPost]
            public async Task<ActionResult<Agent>> CreateAgent(Agent agent)
            {
                try
                {
                    if (agent == null)
                        return BadRequest();

                    var emp = await agentRepository.GetAgentByEmail(agent.Email);

                    if (emp != null)
                    {
                        ModelState.AddModelError("Email", "Agent email already in use");
                        return BadRequest(ModelState);
                    }

                    var createdAgent = await agentRepository.AddAgent(agent);

                    return CreatedAtAction(nameof(GetAgent),
                        new { id = createdAgent.Id }, createdAgent);
                }
                catch (Exception )
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error creating new Agent record");
                }
            }

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
                catch (Exception )
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error updating Agent record");
                }
            }

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
                catch (Exception )
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error deleting Agent record");
                }
            }
        }
    }
