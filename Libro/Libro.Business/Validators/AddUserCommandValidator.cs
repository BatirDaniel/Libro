using FluentValidation;
using Libro.Business.Commands.IdentityCommands;
using System.Text.RegularExpressions;

namespace Libro.Business.Validators
{
    public sealed class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.Fullname)
                .NotEmpty().WithMessage("Fullname cannot be empty")
                .Length(8, 50).WithMessage("Fullname cannot contain less than 8 letters and more than 50")
                .Matches(new Regex(@"^[a - zA - Z] +$")).WithMessage("The fullname may contain only letters and spaces");

            RuleFor(x => x.Username)
                .NotNull().WithMessage("Username is required")
                .NotEmpty().WithMessage("Username cannot be empty")
                .Length(5, 50).WithMessage("Username cannot contain less than 5 characters and more than 50");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email is required")
                .NotEmpty().WithMessage("Email cannot be empty")
                .Matches(new Regex(@"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")).WithMessage("Email is incorrect");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Email is required")
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password length must be at least 8.")
                .MaximumLength(16).WithMessage("Password length must not exceed 16.")
                .Matches(new Regex(@"[A-Z]+")).WithMessage("Password must contain at least one uppercase letter.")
                .Matches(new Regex(@"[a-z]+")).WithMessage("Password must contain at least one lowercase letter.")
                .Matches(new Regex(@"[0-9]+")).WithMessage("Password must contain at least one number.")
                .Matches(new Regex(@"[\!\?\*\.]+")).WithMessage("Password must contain at least one (!? *.).");

            RuleFor(x => x.Telephone)
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
                .Matches(new Regex(@"^\+39\s\d{2,3}\s\d{6,7}$")).WithMessage("Phone Number not valid") //Validation for Italy
                .Matches(new Regex(@"^\+373\s\d{2}\s\d{3}\s\d{3}$")).WithMessage("Phone Number not valid"); //Validation for Republic of Moldova

            RuleFor(x => x.IdUserType)
                .NotEmpty().WithMessage("Role cannot be empty")
                .NotNull().WithMessage("Role is required");
        }
    }
}
