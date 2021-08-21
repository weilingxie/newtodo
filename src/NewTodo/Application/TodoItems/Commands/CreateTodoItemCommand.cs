using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Domain.Constants;
using NewTodo.Domain.Models;
using NewTodo.Infrastructure;

namespace NewTodo.Application.TodoItems.Commands
{
    public class CreateTodoItemCommand : IRequest<TodoItem>
    {
        public NewTodoInput TodoInput { get; }

        public CreateTodoItemCommand(NewTodoInput todoInput)
        {
            TodoInput = todoInput;
        }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItem>
    {
        private readonly ITodoRepository _todoRepository;

        public CreateTodoItemCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoItem> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var newTodoInput = request.TodoInput;
            var todoItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                UserId = newTodoInput.UserId,
                Title = newTodoInput.Title,
                State = TodoState.Todo
            };

            await _todoRepository.CreateTodoItem(todoItem);

            return todoItem;
        }
    }
}