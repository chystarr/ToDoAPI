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
        public async Task<Response> GetToDoTasks()
        {
          if (_context.ToDoTasks == null)
          {
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no tasks found" };
          }
            var returnedToDoTasks = await _context.ToDoTasks.ToListAsync();
            return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedToDoTasks = returnedToDoTasks };
        }

        // GET: api/tasks/fromCategory/5
        [HttpGet("fromCategory/{categoryId}")]
        public async Task<Response> GetToDoTasksFromCategory(int categoryId)
        {
          if (_context.ToDoTasks == null)
          {
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no tasks found" };
          }
          var returnedToDoTasks = await _context.ToDoTasks.Where(toDoTask => toDoTask.CategoryId == categoryId).ToListAsync();
          return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedToDoTasks = returnedToDoTasks };
        }

        // GET: api/tasks/incomplete
        [HttpGet("incomplete")]
        public async Task<Response> GetIncompleteToDoTasks()
        {
            if (_context.ToDoTasks == null)
            {
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no tasks found" };
            }
            var returnedToDoTasks = await _context.ToDoTasks.Where(toDoTask => toDoTask.IsComplete == false).ToListAsync();
            return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedToDoTasks = returnedToDoTasks };
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<Response> PutToDoTask(int id, ToDoTask toDoTask)
        {
            if (id != toDoTask.ToDoTaskId)
            {
                return new Response { StatusCode = 400, StatusDescription = "API call failed - incorrect ID" };
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
                    return new Response { StatusCode = 404, StatusDescription = "API call failed - no task found with that ID" };
                }
                else
                {
                    throw;
                }
            }
            return new Response { StatusCode = 204, StatusDescription = "API call successful" };
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<Response> PostToDoTask(ToDoTask toDoTask)
        {
          if (_context.ToDoTasks == null)
          {
                return new Response { StatusCode = 400, StatusDescription = "API call failed - no tasks table found" };
            }
            _context.ToDoTasks.Add(toDoTask);
            await _context.SaveChangesAsync();
            return new Response { StatusCode = 201, StatusDescription = "API call successful" };
        }

        private bool ToDoTaskExists(int id)
        {
            return (_context.ToDoTasks?.Any(e => e.ToDoTaskId == id)).GetValueOrDefault();
        }
    }
}
