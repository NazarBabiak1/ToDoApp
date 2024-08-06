using FluentValidation;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Api.Validators
{
    public class UpdateAssigneeDtoValidator : AbstractValidator<UpdateAssigneeDto>
    {
        public UpdateAssigneeDtoValidator()
        {
            RuleFor(x => x.AssigneeId)
                .GreaterThan(0).WithMessage("Assignee ID is required.");
        }
    }
}
