using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Infrastructure;
using NewTodo.Domain.Models;

namespace NewTodo.Application.TodoItems.Commands
{
    public class CreateTodoItemCommand : IRequest<Guid>
    {
        public NewTodoInput TodoInput { get; }

        public CreateTodoItemCommand(NewTodoInput todoInput)
        {
            TodoInput = todoInput;
        }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
    {
        private readonly ITodoRepository _todoRepository;

        public CreateTodoItemCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var newTodoInput = request.TodoInput;
            var todoItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                UserId = newTodoInput.UserId,
                Title = newTodoInput.Title,
                State = "todo"
            };

            await _todoRepository.CreateTodoItem(todoItem);

            return todoItem.Id;
        }
    }
}