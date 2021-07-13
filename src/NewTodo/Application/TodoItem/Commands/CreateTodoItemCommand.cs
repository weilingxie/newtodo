using MediatR;
using NewTodo.Application.TodoItem.Models;

namespace NewTodo.Application.TodoItem.Commands
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public readonly NewTodoInput TodoInput;

        public CreateTodoItemCommand(NewTodoInput todoInput)
        {
            TodoInput = todoInput;
        }
    }
}