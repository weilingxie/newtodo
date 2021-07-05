using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NewTodo.Application.TodoItem.Commands;
using NewTodo.Application.TodoItem.Models;
using NewTodo.Controllers;
using Xunit;
using static NewTodo.Test.Helpers.TodoItemGenerator;

namespace NewTodo.Test.Controllers
{
    [Trait("Category","UnitTest")]
    public class TodoControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TodoController _controller;
        private readonly NewTodoInput _validInput;
        private readonly Mock<ILogger<TodoController>> _logger;
        public TodoControllerTests()
        {
            _logger = new Mock<ILogger<TodoController>>();
            _mediatorMock = new Mock<IMediator>();
            _validInput = CreateValidNewTodoInputFaker().Generate();
            _controller = new TodoController(_mediatorMock.Object, _logger.Object);
        }
        
        [Fact]
        public async Task ShouldCallHandler_WhenModelStateIsValid()
        {
            await _controller.CreateTodoItem(_validInput, CancellationToken.None);
            
            _mediatorMock.Verify(
                m=>m.Send(
                    It.Is<CreateTodoItemCommand>(c=>c._todoInput == _validInput),CancellationToken.None),
                Times.Once());
        }
    }
}