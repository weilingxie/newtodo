using MediatR;
using NewTodo.Application.TodoItem.Models;

namespace NewTodo.Application.TodoItem.Commands
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public NewTodoInput TodoInput { get; }

        public CreateTodoItemCommand(NewTodoInput todoInput)
        {
            TodoInput = todoInput;
        }
    }
}