using Todo.Models;

namespace Todo.ViewModels
{
    public class TodoViewModel
    {
        // public int Id { get; set; }
        // public string Name { get; set; }
        public List<TodoItem> TodoList { get; set; }
        public TodoItem Todo { get; set; }
    }
}