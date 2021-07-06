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
        
        public static Faker<NewTodoInput> CreateEmptyUserIdNewTodoInputFaker()
        {
            var faker = new Faker<NewTodoInput>()
                .RuleFor(t => t.UserId, f => Guid.Empty.ToString())
                .RuleFor(t => t.Title, f => f.Lorem.Text());
            return faker;
        }
        
        public static Faker<NewTodoInput> CreateNullUserIdNewTodoInputFaker()
        {
            var faker = new Faker<NewTodoInput>()
                .RuleFor(t => t.UserId, f => null)
                .RuleFor(t => t.Title, f => f.Lorem.Text());
            return faker;
        }
        
        public static Faker<NewTodoInput> CreateNullTitleNewTodoInputFaker()
        {
            var faker = new Faker<NewTodoInput>()
                .RuleFor(t => t.UserId, f => Guid.NewGuid().ToString())
                .RuleFor(t => t.Title, f => null);
            return faker;
        }
        
        public static Faker<NewTodoInput> CreateEmptyTitleNewTodoInputFaker()
        {
            var faker = new Faker<NewTodoInput>()
                .RuleFor(t => t.UserId, f => Guid.NewGuid().ToString())
                .RuleFor(t => t.Title, f => "");
            return faker;
        }
    }
}