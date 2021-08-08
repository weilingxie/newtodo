using System;
using Bogus;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Domain.Models;

namespace NewTodo.Test.Helpers
{
    public static class TodoItemGenerator
    {
        public static Faker<NewTodoInput> CreateValidNewTodoInputFaker()
        {
            var faker = new Faker<NewTodoInput>()
                .RuleFor(t => t.UserId, f => Guid.NewGuid())
                .RuleFor(t => t.Title, f => f.Lorem.Text());
            return faker;
        }

        public static Faker<TodoItem> CreateValidTodoFaker()
        {
            var faker = new Faker<TodoItem>()
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.UserId, f => Guid.NewGuid())
                .RuleFor(t => t.Title, f => f.Lorem.Word())
                .RuleFor(t => t.State, f => "todo")
                .RuleFor(t => t.CreatedAt, f => DateTime.Now)
                .RuleFor(t => t.LastUpdatedAt, f => DateTime.Now);

            return faker;
        }
    }
}