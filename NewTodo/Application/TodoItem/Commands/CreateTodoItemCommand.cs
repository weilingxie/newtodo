using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NewTodo.Application.TodoItem.Models;

namespace NewTodo.Application.TodoItem.Commands
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public NewTodoInput _todoInput;

        public CreateTodoItemCommand(NewTodoInput todoInput)
        {
            _todoInput = todoInput;
        }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}