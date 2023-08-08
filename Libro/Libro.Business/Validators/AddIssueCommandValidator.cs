using FluentValidation;
using Libro.Business.Commands.IssueCommands;

namespace Libro.Business.Validators
{
    public sealed class AddIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        public AddIssueCommandValidator()
        {
            RuleFor(x => x.IdPos);

            RuleFor(x => x.IdType);

            RuleFor(x => x.IdSubType);

            RuleFor(x => x.Priority);

            RuleFor(x => x.IdStatus);

            RuleFor(x => x.Memo);

            RuleFor(x => x.IdUserCreated);

            RuleFor(x => x.IdAssigned);

            RuleFor(x => x.Description);

            RuleFor(x => x.AssignedDate);

            RuleFor(x => x.CreationDate);

            RuleFor(x => x.ModifDate);

            RuleFor(x => x.Solution);

            //Need to implement validation for all properties
        }
    }
}
