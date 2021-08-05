using System;

namespace NewTodo.Domain.Models
{
    public class EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}