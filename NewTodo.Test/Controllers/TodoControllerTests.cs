using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Castle.Core.Internal;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NewTodo.Application.TodoItem.Commands;
using NewTodo.Application.TodoItem.Models;
using NewTodo.Application.TodoItem.Validators;
using NewTodo.Controllers;
using Xunit;
using static System.Guid;
using static NewTodo.Test.Helpers.TodoItemGenerator;

namespace NewTodo.Test.Controllers
{
    [Trait("Category", "UnitTest")]
    public class TodoControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TodoController _controller;
        private readonly NewTodoInput _validInput;
        private readonly NewTodoInputValidator _newTodoInputValidator;
        private const string ValidTitle = "title";
        private const string ValidGuid = "5C60F693-BEF5-E011-A485-80EE7300C695";
        private const string EmptyGuid = "00000000-0000-0000-0000-000000000000";

        public TodoControllerTests()
        {
            _newTodoInputValidator = new NewTodoInputValidator();
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
        public async Task ShouldReturnNoContent_IfMediatorIsAbleToSendWithoutErrors()
        {
            var action = await _controller.CreateTodoItem(_validInput, CancellationToken.None);

            Assert.IsType<NoContentResult>(action);
        }

        [Theory]
        [InlineData(EmptyGuid, null)]
        [InlineData(ValidGuid, null)]
        [InlineData(EmptyGuid, ValidTitle)]
        [InlineData(EmptyGuid, "")]
        public async Task ShouldReturnInvalid_IfProvideInvalidNewTodoInput(string userIdString, string title)
        {
            var invalidInput = new NewTodoInput
            {
                UserId = new Guid(userIdString),
                Title = title
            };
            var result = await _newTodoInputValidator.TestValidateAsync(invalidInput);
            Assert.False(result.IsValid);
        }
    }
}