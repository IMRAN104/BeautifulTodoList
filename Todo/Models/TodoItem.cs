using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{

    public class TodoItem
    {
        // [Key]
        // [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int MyProperty { get; set; }
    }
    enum Priority
    {
        Low = 1,
        Medium = 3,
        High = 5
    }
}