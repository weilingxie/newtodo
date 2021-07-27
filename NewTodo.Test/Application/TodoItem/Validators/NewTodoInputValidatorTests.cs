using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Application.TodoItems.Validators;
using Xunit;

namespace NewTodo.Test.Application.TodoItem.Validators
{
    [Trait("Category", "UnitTest")]
    public class NewTodoInputValidatorTests
    {
        private readonly NewTodoInputValidator _newTodoInputValidator;
        private const string ValidTitle = "title";

        public NewTodoInputValidatorTests()
        {
            _newTodoInputValidator = new NewTodoInputValidator();
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