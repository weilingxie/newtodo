using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewTodo.Application.TodoItem.Commands;
using NewTodo.Application.TodoItem.Models;

namespace NewTodo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateTodoItem(NewTodoInput todoInput, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest();

            var command = new CreateTodoItemCommand(todoInput);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}