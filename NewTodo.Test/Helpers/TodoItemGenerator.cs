using System;
using Bogus;
using NewTodo.Application.TodoItem.Models;

namespace NewTodo.Test.Helpers
{
    public static class TodoItemGenerator
    {
        public static Faker<NewTodoInput> CreateValidNewTodoInputFaker()
        {
            var faker = new Faker<NewTodoInput>()
                .RuleFor(t => t.UserId, f => Guid.NewGuid().ToString())
                .RuleFor(t => t.Title, f => f.Lorem.Text());
            return faker;
        }
    }
}