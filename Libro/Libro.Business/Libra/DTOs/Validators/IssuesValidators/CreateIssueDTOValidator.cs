using FluentValidation;
using Libro.Business.Libra.DTOs.IssueDTOs;

namespace Libro.Business.Libra.DTOs.Validators.IssuesValidators
{
    public class CreateIssueDTOValidator : AbstractValidator<CreateIssueDTO>
    {
        public CreateIssueDTOValidator()
        {
            RuleFor(x => x.IdPos)
             .NotNull().WithMessage("Pos is required")
             .NotEmpty().WithMessage("Pos cannot be empty");

            RuleFor(x => x.IdType)
                .NotNull().WithMessage("Type is required");

            RuleFor(x => x.IdPriority)
                .NotNull().WithMessage("Type is required");

            RuleFor(x => x.IdStatus)
                .NotNull().WithMessage("Status is required");

            RuleFor(x => x.Memo)
                .NotEmpty().WithMessage("Memo cannot be empty");

            RuleFor(x => x.IdUserCreated)
                .NotNull().WithMessage("User is required");

            RuleFor(x => x.IdUsersAsigned)
                .NotNull().WithMessage("User is required");

            RuleFor(x => x.Solution)
                .Length(0, 500).WithMessage("Solution must be between 0 and 500 characters");

            RuleFor(x => x.Description)
                .Length(0, 500).WithMessage("Solution must be between 0 and 500 characters");
        }
    }
}
