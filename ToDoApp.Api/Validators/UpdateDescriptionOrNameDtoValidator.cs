using FluentValidation;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Api.Validators
{
    public class UpdateDescriptionOrNameDtoValidator : AbstractValidator<UpdateDescriptionOrNameDto>
    {
        public UpdateDescriptionOrNameDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Title can't be longer than 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description can't be longer than 500 characters.");
        }
    }

}
