using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Domain.Constants;
using NewTodo.Domain.Models;
using NewTodo.Infrastructure;

namespace NewTodo.Application.TodoItems.Commands
{
    public class CreateTodoItemCommand : IRequest<TodoOutput>
    {
        public NewTodoInput TodoInput { get; }

        public CreateTodoItemCommand(NewTodoInput todoInput)
        {
            TodoInput = todoInput;
        }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoOutput>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public CreateTodoItemCommandHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<TodoOutput> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
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

            return await Task.FromResult(_mapper.Map<TodoOutput>(todoItem));
        }
    }
}