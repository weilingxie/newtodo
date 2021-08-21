using System.Threading;
using System.Threading.Tasks;
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

            var createdTodoItem = await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}