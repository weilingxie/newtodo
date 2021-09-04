using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NewTodo.Application.TodoItems.Commands;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Controllers;
using NewTodo.Domain.Models;
using Xunit;
using static NewTodo.Test.Helpers.TodoItemGenerator;
using static NewTodo.Test.Helpers.LoggerMockHelper;

namespace NewTodo.Test.Controllers
{
    [Trait("Category", "UnitTest")]
    public class TodoControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TodoController _controller;
        private readonly NewTodoInput _validInput;
        private readonly Mock<ILogger<TodoController>> _loggerMock;
        private readonly TodoItem _validTodo;

        public TodoControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<TodoController>>();
            _validInput = CreateValidNewTodoInputFaker().Generate();
            _validTodo = CreateValidTodoFaker().Generate();
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
        public async Task ShouldReturnOk_IfMediatorIsAbleToSendWithoutErrors()
        {
            var actionResult = await _controller.CreateTodoItem(_validInput, CancellationToken.None);

            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task ShouldLogTrace_WhenModelStateIsValid()
        {
            await _controller.CreateTodoItem(_validInput, CancellationToken.None);

            VerifyLogger(_loggerMock, LogLevel.Trace, "Begin: Create todo item");
        }

        [Fact]
        public async Task ShouldCallSend_WhenCallingCreateTodoItem()
        {
            await _controller.CreateTodoItem(_validInput, CancellationToken.None);

            _mediatorMock.Verify(
                m => m.Send(
                    It.IsAny<CreateTodoItemCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}