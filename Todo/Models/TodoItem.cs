namespace Todo.Models;
// {
//     public class TodoItem
//     {
//         // [Key]
//         // [Required]
//         public TodoItem(string name, Priority priority)
//         {
//             Name = name;
//             CreatedAt = DateTime.Now;
//             Priority = priority;
//         }
//         public TodoItem()
//         {
//         }
//         public int Id { get; set; }
//
//         public string Name { get; set; }
//         public DateTime CreatedAt { get; set; }
//         public DateTime? ModifiedAt { get; set; }
//         public Priority Priority { get; set; }
//     }
//
//     public enum Priority
//     {
//         Low = 1,
//         Medium = 3,
//         High = 5
//     }
// }

public class TodoItem
{
    public int Id { get; set; }
    public string Name { get; set; }
}
