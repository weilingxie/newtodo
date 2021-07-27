using System;

namespace NewTodo.Domain.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
    }
}