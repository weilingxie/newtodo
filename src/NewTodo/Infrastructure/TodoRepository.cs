using System;
using System.Data;
using System.Threading.Tasks;
using NewTodo.Domain.Models;

namespace NewTodo.Infrastructure
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IDbConnection _connection;

        public TodoRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public Task CreateTodoItem(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }
    }
}