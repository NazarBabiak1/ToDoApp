using FluentValidation;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Api.Validators
{

    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title can't be longer than 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description can't be longer than 500 characters.");

            RuleFor(x => x.BoardId)
                .GreaterThan(0).WithMessage("Board ID is required.");

            RuleFor(x => x.AssigneeId)
                .GreaterThan(0).WithMessage("Assignee ID is required.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now).WithMessage("Due date must be in the future.");
        }
    }
}