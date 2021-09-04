using System.Threading.Tasks;
using Dapper;
using NewTodo.Infrastructure;
using NewTodo.Test.Helpers;
using Xunit;

namespace NewTodo.Test.Infrastructure
{
    [Trait("Category", "DbTest")]
    public class TodoRepositoryTests : SqlDbTestBase
    {
        private TodoRepository TodoRepository { get; set; }

        public TodoRepositoryTests()
        {
            TodoRepository = new TodoRepository(Connection);
        }

        [Fact]
        public async Task ShouldCreateRecord_WhenRecordIsValid()
        {
            await ResetDatabase();

            var todoItem = TodoItemGenerator.CreateValidTodoFaker().Generate();
            await TodoRepository.CreateTodoItem(todoItem);

            var result = await Connection.QueryFirstAsync(
                @"SELECT * FROM TodoItems WHERE Id=@Id",
                new {Id = todoItem.Id}
            );

            Assert.Equal(todoItem.Id, result.Id);
            Assert.Equal(todoItem.UserId, result.UserId);
            Assert.Equal(todoItem.Title, result.Title);
            Assert.Equal(todoItem.State, result.State);
            Assert.Equal(todoItem.CreatedAt, result.CreatedAt);
            Assert.Equal(todoItem.LastUpdatedAt, result.LastUpdatedAt);
        }
    }
}