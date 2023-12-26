﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace To_Do_App.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Task")]
        public string taskName { get; set; }

        [StringLength(255)]
        [DisplayName("Description")]
        public string description { get; set; }

        [Required]
        [DisplayName("Priority")]
        [EnumDataType(typeof(PriorityLevel))]
        public string priorityLevel { get; set; }

        [DisplayName("Due Date")]
        public DateTime? dueDate { get; set; }

    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}
