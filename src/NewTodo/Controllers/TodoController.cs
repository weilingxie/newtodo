using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewTodo.Application.TodoItems.Commands;
using NewTodo.Application.TodoItems.Models;

namespace NewTodo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TodoController> _logger;
        private readonly IMapper _mapper;

        public TodoController(IMediator mediator, ILogger<TodoController> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateTodoItem(NewTodoInput todoInput, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest();

            _logger.LogTrace("Begin: Create todo item");

            var command = new CreateTodoItemCommand(todoInput);

            var createdTodoItem = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<TodoOutput>(createdTodoItem));
        }
    }
}