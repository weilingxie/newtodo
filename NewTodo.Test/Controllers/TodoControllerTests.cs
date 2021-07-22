using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NewTodo.Application.TodoItem.Commands;
using NewTodo.Application.TodoItem.Models;
using NewTodo.Controllers;
using Xunit;
using static NewTodo.Test.Helpers.TodoItemGenerator;

namespace NewTodo.Test.Controllers
{
    [Trait("Category", "UnitTest")]
    public class TodoControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TodoController _controller;
        private readonly NewTodoInput _validInput;

        public TodoControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _validInput = CreateValidNewTodoInputFaker().Generate();
            _controller = new TodoController(_mediatorMock.Object);
        }

        [Fact]
        public async Task ShouldCallHandler_WhenModelStateIsValid()
        {
            await _controller.CreateTodoItem(_validInput, CancellationToken.None);

            _mediatorMock.Verify(
                m => m.Send(
                    It.Is<CreateTodoItemCommand>(c => c.TodoInput == _validInput), CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async Task ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("key", "error message");

            var action = await _controller.CreateTodoItem(new NewTodoInput(), CancellationToken.None);

            Assert.IsType<BadRequestResult>(action);
        }

        [Fact]
        public async Task ShouldReturnNoContent_IfMediatorIsAbleToSendWithoutErrors()
        {
            var action = await _controller.CreateTodoItem(_validInput, CancellationToken.None);

            Assert.IsType<NoContentResult>(action);
        }
    }
}