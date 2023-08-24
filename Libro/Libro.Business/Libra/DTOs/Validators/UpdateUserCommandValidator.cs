using FluentValidation;
using Libro.Business.Commands.IdentityCommands;
using System.Text.RegularExpressions;

namespace Libro.Business.Libra.DTOs.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Firstname)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotEmpty().WithMessage("Firstname cannot be empty");

            RuleFor(x => x.Lastname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Lastname cannot be empty");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Fullname cannot be empty")
                .Length(8, 50).WithMessage("Fullname cannot contain less than 8 letters and more than 50");
            //.Matches(new Regex(@"^[a-zA-Z ]+$")).WithMessage("Fullname may contain only letters and spaces");

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.StopOnFirstFailure);
            //.NotNull().WithMessage("Username is required")
            //.NotEmpty().WithMessage("Username cannot be empty")
            //.Length(5, 50).WithMessage("Username cannot contain less than 5 characters and more than 50");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Email is required")
                .NotEmpty().WithMessage("Email cannot be empty")
                .Matches(new Regex(@"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")).WithMessage("Email is incorrect");

            RuleFor(x => x.Telephone)
                .Cascade(CascadeMode.StopOnFirstFailure);
            //.MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            //.MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.");
            //.Matches(new Regex(@"^\+39\s\d{2,3}\s\d{6,7}$")).WithMessage("Phone Number not valid") //Validation for Italy
            //.Matches(new Regex(@"^\+373\s\d{2}\s\d{3}\s\d{3}$")).WithMessage("Phone Number not valid"); //Validation for Republic of Moldova

            RuleFor(x => x.Role.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Role cannot be empty")
                .NotNull().WithMessage("Role is required");
        }
    }
}
