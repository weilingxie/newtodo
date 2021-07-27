using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Infrastructure;
using NewTodo.Domain.Models;

namespace NewTodo.Application.TodoItems.Commands
{
    public class CreateTodoItemCommand : IRequest
    {
        public NewTodoInput TodoInput { get; }

        public CreateTodoItemCommand(NewTodoInput todoInput)
        {
            TodoInput = todoInput;
        }
    }

    public class CreateTodoItemCommandHandler : AsyncRequestHandler<CreateTodoItemCommand>
    {
        private readonly ITodoRepository _todoRepository;

        public CreateTodoItemCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        protected override async Task Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var newTodoInput = request.TodoInput;
            var todoItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                UserId = newTodoInput.UserId,
                Title = newTodoInput.Title,
                State = "Todo"
            };

            await _todoRepository.CreateTodoItem(todoItem);
        }
    }
}