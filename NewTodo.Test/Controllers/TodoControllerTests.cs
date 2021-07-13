using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NewTodo.Application.TodoItem.Commands;
using NewTodo.Application.TodoItem.Models;
using NewTodo.Application.TodoItem.Validators;
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
        private readonly NewTodoInput _nullUserIdInput;
        private readonly NewTodoInput _emptyUserIdInput;
        private readonly NewTodoInputValidator _newTodoInputValidator;

        public TodoControllerTests()
        {
            _newTodoInputValidator = new NewTodoInputValidator();
            var logger = new Mock<ILogger<TodoController>>();
            _mediatorMock = new Mock<IMediator>();
            _validInput = CreateValidNewTodoInputFaker().Generate();
            _nullUserIdInput = CreateNullUserIdNewTodoInputFaker().Generate();
            _emptyUserIdInput = CreateEmptyUserIdNewTodoInputFaker().Generate();
            _controller = new TodoController(_mediatorMock.Object, logger.Object);
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
        public async Task ShouldReturnNoContent_IfMediatorIsAbleToSendWithoutErrors()
        {
            var action = await _controller.CreateTodoItem(_validInput, CancellationToken.None);

            Assert.IsType<NoContentResult>(action);
        }

        [Fact]
        public async Task ShouldReturnNull_IfUserIdIsNull()
        {
            var result = await _newTodoInputValidator.ValidateAsync(_emptyUserIdInput);
            Assert.False(result.IsValid);
        }
    }
}