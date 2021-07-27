using System.Threading;
using MediatR;
using Moq;
using NewTodo.Application.TodoItems.Commands;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Infrastructure;
using Xunit;
using static NewTodo.Test.Helpers.TodoItemGenerator;

namespace NewTodo.Test.Application.TodoItem.Commands
{
    public class CreateTodoItemCommandTests
    {
        private readonly Mock<ITodoRepository> _repositoryMock;
        private readonly NewTodoInput _validInput;
        private readonly CreateTodoItemCommand _command;
        private readonly IRequestHandler<CreateTodoItemCommand> _handler;

        public CreateTodoItemCommandTests()
        {
            _repositoryMock = new Mock<ITodoRepository>();
            _validInput = CreateValidNewTodoInputFaker().Generate();
            _command = new CreateTodoItemCommand(_validInput);
            _handler = new CreateTodoItemCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public void ShouldCallRepositoryCreateTodoItem()
        {
            _handler.Handle(_command, CancellationToken.None);

            _repositoryMock.Verify(
                r =>
                    r.CreateTodoItem(It.Is<Domain.Models.TodoItem>(t => t.UserId == _validInput.UserId
                                                                        && t.Title == _validInput.Title
                                                                        && t.State == "Todo")), Times.Once()
            );
        }
    }
}