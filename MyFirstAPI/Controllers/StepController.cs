using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/steps")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly ToDoAPIDBContext _context;

        public StepController(ToDoAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/steps/fromTask/5
        [HttpGet("fromTask/{toDoTaskId}")]
        public async Task<Response> GetStepsFromToDoTask(int toDoTaskId)
        {
          if (_context.Steps == null)
          {
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no steps found" };
            }
            var returnedSteps = await _context.Steps.Where(step => step.ToDoTaskId == toDoTaskId).ToListAsync();
            return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedSteps = returnedSteps };
        }

        // GET: api/steps/fromTask/5/incomplete
        [HttpGet("fromTask/{toDoTaskId}/incomplete")]
        public async Task<Response> GetIncompleteStepsFromTask(int toDoTaskId)
        {
            if (_context.Steps == null)
            {
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no steps found" };
            }
            var returnedSteps = await _context.Steps.Where(step => step.ToDoTaskId == toDoTaskId && step.IsComplete == false).ToListAsync();
            return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedSteps = returnedSteps };
        }

        // PUT: api/steps/5
        [HttpPut("{id}")]
        public async Task<Response> PutStep(int id, Step step)
        {
            if (id != step.StepId)
            {
                return new Response { StatusCode = 400, StatusDescription = "API call failed - incorrect ID" };
            }

            _context.Entry(step).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepExists(id))
                {
                    return new Response { StatusCode = 404, StatusDescription = "API call failed - no step found with that ID" };
                }
                else
                {
                    throw;
                }
            }
            return new Response { StatusCode = 204, StatusDescription = "API call successful" };
        }

        // POST: api/steps
        [HttpPost]
        public async Task<Response> PostStep(Step step)
        {
          if (_context.Steps == null)
          {
                return new Response { StatusCode = 400, StatusDescription = "API call failed - no steps table found" };
            }
            _context.Steps.Add(step);
            await _context.SaveChangesAsync();

            return new Response { StatusCode = 201, StatusDescription = "API call successful" };
        }

        private bool StepExists(int id)
        {
            return (_context.Steps?.Any(e => e.StepId == id)).GetValueOrDefault();
        }
    }
}
