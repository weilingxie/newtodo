using System;

namespace NewTodo.Application.TodoItems.Models
{
    public class TodoOutput
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
    }
}