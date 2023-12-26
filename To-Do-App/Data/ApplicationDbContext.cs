using Microsoft.EntityFrameworkCore;
using To_Do_App.Models;

namespace To_Do_App.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>().HasData(
                new TaskModel { Id = 1, taskName = "Finish Basic Functionality", priorityLevel = "High" },
                new TaskModel { Id = 2, taskName = "Add Additional Features", priorityLevel = "Medium" }
                );
        }
    }
}
