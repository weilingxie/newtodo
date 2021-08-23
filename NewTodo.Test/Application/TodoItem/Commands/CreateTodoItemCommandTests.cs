using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NewTodo.Application.TodoItems.Commands;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Domain.Constants;
using NewTodo.Infrastructure;
using Xunit;
using static NewTodo.Test.Helpers.TodoItemGenerator;

namespace NewTodo.Test.Application.TodoItem.Commands
{
    public class CreateTodoItemCommandTests
    {
        private readonly Mock<ITodoRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly NewTodoInput _validInput;
        private readonly CreateTodoItemCommand _command;
        private readonly IRequestHandler<CreateTodoItemCommand, TodoOutput> _handler;


        public CreateTodoItemCommandTests()
        {
            _repositoryMock = new Mock<ITodoRepository>();
            _mapperMock = new Mock<IMapper>();
            _validInput = CreateValidNewTodoInputFaker().Generate();
            _command = new CreateTodoItemCommand(_validInput);
            _handler = new CreateTodoItemCommandHandler(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void ShouldCallRepositoryCreateTodoItem()
        {
            _handler.Handle(_command, CancellationToken.None);

            _repositoryMock.Verify(
                r =>
                    r.CreateTodoItem(It.Is<Domain.Models.TodoItem>(t => t.UserId == _validInput.UserId
                                                                        && t.Title == _validInput.Title
                                                                        && t.State == TodoState.Todo)), Times.Once()
            );
        }

        [Fact]
        public async Task ShouldReturnValidTodoOutput_WhenProvideValidNewTodoInput()
        {
            var validTodoOutput = new TodoOutput()
            {
                Id = new Guid(),
                UserId = _validInput.UserId,
                Title = _validInput.Title,
                State = TodoState.Todo
            };

            _mapperMock.Setup(m => m.Map<TodoOutput>(It.IsAny<Domain.Models.TodoItem>()))
                .Returns((Domain.Models.TodoItem src) => validTodoOutput);

            var taskResult = await _handler.Handle(_command, CancellationToken.None);

            Assert.Equal(typeof(Guid), taskResult.Id.GetType());
            Assert.Equal(_validInput.UserId, taskResult.UserId);
            Assert.Equal(_validInput.Title, taskResult.Title);
            Assert.Equal(TodoState.Todo, taskResult.State);
        }
    }
}