using System.Data.SqlTypes;
using NewTodo.Infrastructure;
using NewTodo.Test.Helpers;
using Xunit;

namespace NewTodo.Test.Infrastructure
{
    [Trait("Category","DbTest")]
    public class TodoRepositoryTests : SqlDbTestBase
    {
        private TodoRepository TodoRepository { get; set; }
        public TodoRepositoryTests()
        {
            TodoRepository = new TodoRepository(Connection);
        }
    }
}