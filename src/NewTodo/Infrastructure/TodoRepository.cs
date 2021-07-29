using System;
using System.Threading.Tasks;
using NewTodo.Domain.Models;

namespace NewTodo.Infrastructure
{
    public class TodoRepository : ITodoRepository
    {
        public Task CreateTodoItem(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }
    }
}