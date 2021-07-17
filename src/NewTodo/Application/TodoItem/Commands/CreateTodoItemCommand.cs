using MediatR;
using NewTodo.Application.TodoItem.Models;

namespace NewTodo.Application.TodoItem.Commands
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public NewTodoInput TodoInput { get; private set; }

        public CreateTodoItemCommand(NewTodoInput todoInput)
        {
            TodoInput = todoInput;
        }
    }
}