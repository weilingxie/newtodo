using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NewTodo.Application.TodoItems.Commands;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Controllers;
using NewTodo.Test.Helpers;
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
        private readonly Mock<ILogger<TodoController>> _loggerMock;

        public TodoControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<TodoController>>();
            _validInput = CreateValidNewTodoInputFaker().Generate();
            _controller = new TodoController(_mediatorMock.Object, _loggerMock.Object);
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

        [Fact]
        public async Task ShouldLogTrace_WhenModelStateIsValid()
        {
            await _controller.CreateTodoItem(_validInput, CancellationToken.None);
            _loggerMock.VerifyLogging("Begin: Create todo item");
        }

        [Fact]
        public async Task ShouldCallHandler_WhenCallingCreateTodoItem()
        {
            await _controller.CreateTodoItem(_validInput, CancellationToken.None);

            _mediatorMock.Verify(
                m => m.Send(
                    It.IsAny<CreateTodoItemCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}