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
    [Route("api/tasks")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly ToDoAPIDBContext _context;

        public ToDoTaskController(ToDoAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<ToDoTask>>> GetToDoTasks()
        public async Task<Response> GetToDoTasks()
        {
          if (_context.ToDoTasks == null)
          {
                //return NotFound();
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no tasks found" };
          }

            //return await _context.ToDoTasks.ToListAsync();
            var returnedToDoTasks = await _context.ToDoTasks.ToListAsync();
            return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedToDoTasks = returnedToDoTasks };
        }

        // GET: api/tasks/fromCategory/5
        [HttpGet("fromCategory/{categoryId}")]
        //public async Task<ActionResult<ToDoTask>> GetToDoTasksFromCategory(int categoryId)
        //public async Task<ActionResult<IEnumerable<ToDoTask>>> GetToDoTasksFromCategory(int categoryId)
        public async Task<Response> GetToDoTasksFromCategory(int categoryId)
        {
          if (_context.ToDoTasks == null)
          {
                //return NotFound();
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no tasks found" };
          }
          var returnedToDoTasks = await _context.ToDoTasks.Where(toDoTask => toDoTask.CategoryId == categoryId).ToListAsync();
          return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedToDoTasks = returnedToDoTasks };
            /*
            var toDoTask = await _context.ToDoTasks.FindAsync(id);

            if (toDoTask == null)
            {
                return NotFound();
            }

            return toDoTask;
            */
        }

        // GET: api/tasks/incomplete
        [HttpGet("incomplete")]
        //public async Task<ActionResult<IEnumerable<ToDoTask>>> GetIncompleteToDoTasks()
        public async Task<Response> GetIncompleteToDoTasks()
        {
            if (_context.ToDoTasks == null)
            {
                //return NotFound();
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no tasks found" };
            }
            var returnedToDoTasks = await _context.ToDoTasks.Where(toDoTask => toDoTask.IsComplete == false).ToListAsync();
            return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedToDoTasks = returnedToDoTasks };
        }

        // PUT: api/tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoTask(int id, ToDoTask toDoTask)
        {
            if (id != toDoTask.ToDoTaskId)
            {
                return BadRequest();
            }

            _context.Entry(toDoTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoTaskExists(id))
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

        // POST: api/tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //public async Task<ActionResult<ToDoTask>> PostToDoTask(ToDoTask toDoTask)
        public async Task<Response> PostToDoTask(ToDoTask toDoTask)
        {
          if (_context.ToDoTasks == null)
          {
                //return Problem("Entity set 'ToDoAPIDBContext.ToDoTasks'  is null.");
                return new Response { StatusCode = 400, StatusDescription = "API call failed - no tasks table found" };
            }
            _context.ToDoTasks.Add(toDoTask);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetToDoTask", new { id = toDoTask.ToDoTaskId }, toDoTask);
            return new Response { StatusCode = 201, StatusDescription = "API call successful" };
        }

        /*
        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoTask(int id)
        {
            if (_context.ToDoTasks == null)
            {
                return NotFound();
            }
            var toDoTask = await _context.ToDoTasks.FindAsync(id);
            if (toDoTask == null)
            {
                return NotFound();
            }

            _context.ToDoTasks.Remove(toDoTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

        private bool ToDoTaskExists(int id)
        {
            return (_context.ToDoTasks?.Any(e => e.ToDoTaskId == id)).GetValueOrDefault();
        }
    }
}
