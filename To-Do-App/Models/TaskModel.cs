using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace To_Do_App.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Task Name")]
        public string taskName { get; set; }

        [StringLength(255)]
        [DisplayName("Description")]
        public string? description { get; set; }

        [Required]
        [EnumDataType(typeof(PriorityLevel))]
        [DisplayName("Priority")]
        public PriorityLevel priorityLevel { get; set; }

        [DisplayName("Time")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:h:mm tt}")]
        public DateTime? dueDate { get; set; }

        [Required]
        [EnumDataType(typeof(Category))]
        [DisplayName("Category")]
        public Category category { get; set; }

        [DisplayName("Completed")]
        public bool isComplete { get; set; }

    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }

    public enum Category
    {
        Work,
        Home,
        School,
        Appointment,
        Other
    }
}
