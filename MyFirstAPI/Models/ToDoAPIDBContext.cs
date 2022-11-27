using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using ToDoAPI.Models;

namespace ToDoAPI.Models
{
    public class ToDoAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ToDoAPIDBContext(DbContextOptions<ToDoAPIDBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("ToDoDataService");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;
        public DbSet<Step> Steps { get; set; } = null!;
    }
}

