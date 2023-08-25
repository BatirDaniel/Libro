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

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Confirm password must match the password.");

            RuleFor(x => x.Telephone)
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
                .Matches(new Regex(@"^\+373\s\d{2}\s\d{3}\s\d{3}$")).WithMessage("Phone Number not valid"); //Validation for Republic of Moldova

            RuleFor(x => x.Role.Id)
                .NotEmpty().WithMessage("Role cannot be empty")
                .NotNull().WithMessage("Role is required");
        }
    }
}
