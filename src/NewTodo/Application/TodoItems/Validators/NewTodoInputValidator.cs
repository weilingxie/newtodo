using FluentValidation;
using NewTodo.Application.TodoItems.Models;

namespace NewTodo.Application.TodoItems.Validators
{
    public class NewTodoInputValidator : AbstractValidator<NewTodoInput>
    {
        public NewTodoInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Title).NotNull();
        }
    }
}