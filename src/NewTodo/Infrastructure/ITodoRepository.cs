using System.Threading.Tasks;
using NewTodo.Domain.Models;

namespace NewTodo.Infrastructure
{
    public interface ITodoRepository
    {
        Task CreateTodoItem(TodoItem todoItem);
    }
}