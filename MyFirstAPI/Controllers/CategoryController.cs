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
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ToDoAPIDBContext _context;

        public CategoryController(ToDoAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<Response> GetCategories()
        {
          if (_context.Categories == null)
          {
                return new Response { StatusCode = 404, StatusDescription = "API call failed - no categories found" };
          }
            var returnedCategories = await _context.Categories.ToListAsync();
            return new Response { StatusCode = 200, StatusDescription = "API call successful", ReturnedCategories = returnedCategories };
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public async Task<Response> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return new Response { StatusCode = 400, StatusDescription = "API call failed - incorrect ID" };
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    new Response { StatusCode = 404, StatusDescription = "API call failed - no category found with that ID" };
                }
                else
                {
                    throw;
                }
            }

            return new Response { StatusCode = 204, StatusDescription = "API call successful" };
        }

        // POST: api/categories
        [HttpPost]
        public async Task<Response> PostCategory(Category category)
        {
          if (_context.Categories == null)
          {
              return new Response { StatusCode = 400, StatusDescription = "API call failed - no categories table found" };
          }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new Response { StatusCode = 201, StatusDescription = "API call successful" };
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
