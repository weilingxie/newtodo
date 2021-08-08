using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using NewTodo.Domain.Models;

namespace NewTodo.Infrastructure
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IDbConnection _connection;

        public TodoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task CreateTodoItem(TodoItem todoItem)
        {
            SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);

            const string sql = @"
            INSERT INTO [dbo].[TodoItems] (
                   [Id],
                   [UserId],
                   [Title],
                   [State],
                   [CreatedAt],
                   [LastUpdatedAt]
                   )
            VALUES(
                   @Id,
                   @UserId,
                   @Title,
                   @State,
                   @CreatedAt,
                   @LastUpdatedAt
                   )
            ";

            await _connection.ExecuteAsync(sql, new
            {
                todoItem.Id,
                todoItem.UserId,
                todoItem.Title,
                todoItem.State,
                todoItem.CreatedAt,
                todoItem.LastUpdatedAt
            });
        }
    }
}