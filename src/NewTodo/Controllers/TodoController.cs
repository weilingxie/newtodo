using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewTodo.Application.TodoItem.Commands;
using NewTodo.Application.TodoItem.Models;

namespace NewTodo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger<TodoController> _logger;

        public TodoController(IMediator mediator, ILogger<TodoController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateTodoItem(NewTodoInput todoInput, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest();

            _logger.LogTrace("Begin: Create todo item");

            var command = new CreateTodoItemCommand(todoInput);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}