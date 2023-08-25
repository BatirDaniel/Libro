using FluentValidation;
using Libro.Business.Libra.DTOs.IssueDTOs;
using System.Text.RegularExpressions;

namespace Libro.Business.Libra.DTOs.Validators.IssuesValidators
{
    public class UpdateIssueDTOValidator : AbstractValidator<UpdateIssueDTO>
    {
        public UpdateIssueDTOValidator()
        {
            RuleFor(x => x.Pos.Id)
                .NotNull().WithMessage("Pos is required")
                .NotEmpty().WithMessage("Pos cannot be empty");

            RuleFor(x => x.IssueTypes.Id)
                .NotNull().WithMessage("Type is required")
                .NotEmpty().WithMessage("Type cannot be empty");

            RuleFor(x => x.IdSubType)
                .NotEmpty().WithMessage("Subtype cannot be empty");

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority cannot be empty");

            RuleFor(x => x.Status.Id)
                .NotNull().WithMessage("Status is required");

            RuleFor(x => x.Memo)
                .NotEmpty().WithMessage("Memo cannot be empty");

            RuleFor(x => x.User.Id)
                .NotNull().WithMessage("User is required");

            RuleFor(x => x.UserAsigned.Id)
                .NotNull().WithMessage("User is required");

            RuleFor(x => x.Solution)
                .Length(0, 500).WithMessage("Solution must be between 0 and 500 characters");

            RuleFor(x => x.Description)
                .Length(0, 500).WithMessage("Solution must be between 0 and 500 characters");
        }
    }
}
