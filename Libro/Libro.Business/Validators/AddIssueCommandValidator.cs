using FluentValidation;
using Libro.Business.Commands.IssueCommands;

namespace Libro.Business.Validators
{
    public sealed class AddIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        public AddIssueCommandValidator()
        {
            RuleFor(x => x.IdPos)
                .NotEmpty().WithMessage("POS is required");

            RuleFor(x => x.IdType)
                .NotEmpty().WithMessage("Issue type is required");

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required");

            RuleFor(x => x.IdStatus)
                .NotEmpty().WithMessage("Status is required");

            RuleFor(x => x.IdUserCreated)
                .NotEmpty().WithMessage("User is required");

            RuleFor(x => x.IdAssigned)
                .NotEmpty().WithMessage("User is required");

            RuleFor(x => x.Description)
                .Length(0, 150).WithMessage("Description cannot have more than 150 characters");

            RuleFor(x => x.AssignedDate)
                .LessThan(DateTime.UtcNow).WithMessage("Invalid assigned date");
        }
    }
}
