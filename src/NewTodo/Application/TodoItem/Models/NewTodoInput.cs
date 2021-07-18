using System;

namespace NewTodo.Application.TodoItem.Models
{
    public class NewTodoInput
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
    }
}