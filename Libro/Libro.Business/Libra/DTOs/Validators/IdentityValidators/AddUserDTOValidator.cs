using FluentValidation;
using Libro.Business.Libra.DTOs.IdentityDTOs;
using System.Text.RegularExpressions;

namespace Libro.Business.Libra.DTOs.Validators.IdentityValidators
{
    public class AddUserDTOValidator : AbstractValidator<AddUserDTO>
    {
        [Obsolete]
        public AddUserDTOValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("Firstname cannot be empty");

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Lastname cannot be empty");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .Length(8, 50).WithMessage("Name cannot contain less than 8 letters and more than 50")
                .Matches(new Regex(@"^[a-zA-Z ]+$")).WithMessage("Name may contain only letters and spaces");

            RuleFor(x => x.Username)
                .NotNull().WithMessage("Username is required")
                .NotEmpty().WithMessage("Username cannot be empty")
                .Length(5, 50).WithMessage("Username cannot contain less than 5 characters and more than 50");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email is required")
                .NotEmpty().WithMessage("Email cannot be empty")
                .Matches(new Regex(@"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")).WithMessage("Email is incorrect");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).*$")
                .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Confirm password must match the password.");

            RuleFor(x => x.Telephone)
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20).WithMessage("PhoneNumber must not exceed 20 characters.");

            //    .Matches(new Regex(@"^\+\d{2,4}\d{8,12}$")).WithMessage("Phone Number not valid. Use the format: +[country code][phone number]");

            //RuleFor(x => x.Telephone)
            //    .Matches(new Regex(@"^\+373\d{8}$")).WithMessage("Moldova: Phone Number not valid.");

            //RuleFor(x => x.Telephone)
            //    .Matches(new Regex(@"^\+39\d{9,10}$")).WithMessage("Italy: Phone Number not valid.");

            //RuleFor(x => x.Telephone)
            //    .Matches(new Regex(@"^\+40\d{9}$")).WithMessage("Romania: Phone Number not valid.");

            //RuleFor(x => x.Telephone)
            //    .Matches(new Regex(@"^\+44\d{10}$")).WithMessage("England: Phone Number not valid.");

            RuleFor(x => x.Role.Id)
                .NotEmpty().WithMessage("Role cannot be empty")
                .NotNull().WithMessage("Role is required");
        }
    }
}
