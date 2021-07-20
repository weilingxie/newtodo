using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        private readonly NewTodoInputValidator _newTodoInputValidator;
        private const string ValidTitle = "title";

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
        [MemberData(nameof(InvalidNewTodoInput))]
        public async Task ShouldReturnInvalid_IfProvideInvalidNewTodoInput(NewTodoInput invalidInput)
        {
            var result = await _newTodoInputValidator.TestValidateAsync(invalidInput);
            Assert.False(result.IsValid);
        }

        public static IEnumerable<object[]> InvalidNewTodoInput =>
            new List<object[]>
            {
                new object[] {new NewTodoInput() {UserId = Guid.Empty, Title = ValidTitle}},
                new object[] {new NewTodoInput() {UserId = Guid.Empty, Title = string.Empty}},
                new object[] {new NewTodoInput() {UserId = Guid.Empty, Title = null}},
                new object[] {new NewTodoInput() {UserId = Guid.NewGuid(), Title = string.Empty}},
                new object[] {new NewTodoInput() {UserId = Guid.NewGuid(), Title = null}}
            };
    }
}