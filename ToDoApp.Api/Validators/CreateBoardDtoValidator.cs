using FluentValidation;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Api.Validators
{
    public class CreateBoardDtoValidator : AbstractValidator<CreateBoardDto>
    {
        public CreateBoardDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Board name is required.")
                .MaximumLength(100).WithMessage("Board name can't be longer than 100 characters.");
        }
    }
}
