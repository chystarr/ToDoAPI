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


        // test
        // GET: api/steps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Step>>> GetSteps()
        {
          if (_context.Steps == null)
          {
              return NotFound();
          }
            return await _context.Steps.ToListAsync();
        }
        

        // GET: api/steps/fromTask/5
        [HttpGet("fromTask/{toDoTaskId}")]
        public async Task<Response> GetStepsFromToDoTask(int toDoTaskId)
        //public async Task<ActionResult<IEnumerable<Step>>> GetStepsFromTask(int toDoTaskId)
        //public async Task<ActionResult<Step>> GetStep(int id)
        {
          if (_context.Steps == null)
          {
                //return NotFound();
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no steps found" };
            }
            var returnedSteps = await _context.Steps.Where(step => step.ToDoTaskId == toDoTaskId).ToListAsync();
            //var returnedSteps = await _context.Steps.ToListAsync();
            return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedSteps = returnedSteps };
            /*
            var step = await _context.Steps.FindAsync(id);

            if (step == null)
            {
                return NotFound();
            }

            return step;
            */
        }

        // GET: api/steps/fromTask/5/incomplete
        [HttpGet("fromTask/{toDoTaskId}/incomplete")]
        //public async Task<ActionResult<IEnumerable<Step>>> GetIncompleteStepsFromTask(int toDoTaskId)
        public async Task<Response> GetIncompleteStepsFromTask(int toDoTaskId)
        {
            if (_context.Steps == null)
            {
                //return NotFound();
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no steps found" };
            }
            var returnedSteps = await _context.Steps.Where(step => step.ToDoTaskId == toDoTaskId && step.IsComplete == false).ToListAsync();
            return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedSteps = returnedSteps };
        }

        // PUT: api/steps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutStep(int id, Step step)
        public async Task<Response> PutStep(int id, Step step)
        {
            if (id != step.StepId)
            {
                //return BadRequest();
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
                    //return NotFound();
                    return new Response { StatusCode = 404, StatusDescription = "API call failed - no step found with that ID" };
                }
                else
                {
                    throw;
                }
            }

            // return NoContent();
            return new Response { StatusCode = 204, StatusDescription = "API call successful" };
        }

        // POST: api/steps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //public async Task<ActionResult<Step>> PostStep(Step step)
        public async Task<Response> PostStep(Step step)
        {
          if (_context.Steps == null)
          {
                //return Problem("Entity set 'ToDoAPIDBContext.Steps'  is null.");
                return new Response { StatusCode = 400, StatusDescription = "API call failed - no steps table found" };
            }
            _context.Steps.Add(step);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetStep", new { id = step.StepId }, step);
            return new Response { StatusCode = 201, StatusDescription = "API call successful" };
        }

        /*
        // DELETE: api/steps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStep(int id)
        {
            if (_context.Steps == null)
            {
                return NotFound();
            }
            var step = await _context.Steps.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }

            _context.Steps.Remove(step);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

        private bool StepExists(int id)
        {
            return (_context.Steps?.Any(e => e.StepId == id)).GetValueOrDefault();
        }
    }
}
