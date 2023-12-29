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
                new TaskModel { 
                    Id = 1, 
                    taskName = "Finish Basic Functionality", 
                    priorityLevel = PriorityLevel.High,
                    dueDate = DateTime.MinValue.Add(new TimeSpan(15, 30, 0)),
                    description = "Create basic CRUD functionality so ToDo list is usable", 
                    category = Category.Work },
                new TaskModel { 
                    Id = 2, 
                    taskName = "Add Additional Features", 
                    priorityLevel = PriorityLevel.High,
                    dueDate = DateTime.MinValue.Add(new TimeSpan(10, 00, 0)),
                    description = "Add additional user-friendly features to enhance ToDo App", 
                    category = Category.Work }
                );
        }
    }
}
