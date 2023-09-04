using FluentValidation;
using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Validators.ValidatorSuport;
using System.Text.RegularExpressions;

namespace Libro.Business.Libra.DTOs.Validators.POSsValidators
{
    public class UpdatePOSDTOValidator : AbstractValidator<UpdatePOSDTO>
    {
        public ValidateDaysClosed? _validator = new ValidateDaysClosed();

        [Obsolete]
        public UpdatePOSDTOValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .Length(5, 80).WithMessage("Name cannot contain less than 8 letters and more than 50");

            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("Telephone cannot be empty.")
                .Length(10, 20).WithMessage("Telephone cannot contain less than 10 digit and more than 20");

            RuleFor(x => x.Cellphone)
                .Length(10, 20).WithMessage("Cellphone cannot contain less than 10 digit and more than 20");

            RuleFor(x => x.Address)
                .Length(5, 80).WithMessage("Adress must not be less than 5 letters and more than 80");

            RuleFor(x => x.City.Id)
                .NotEmpty()
                .WithMessage("City cannot be empty.");

            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Brand cannot be empty.");

            RuleFor(x => x.Brand)
                .NotEmpty()
                .WithMessage("Brand cannot be empty.");

            RuleFor(x => x.ConnectionType.Id)
                .NotEmpty()
                .WithMessage("Connection type cannot be empty.");

            RuleFor(x => x.MorningOpening)
                .LessThan(x => x.MorningClosing)
                .WithMessage("Morning opening time must be greater than or equal to morning closing time.");

            RuleFor(x => x.MorningClosing)
                .GreaterThanOrEqualTo(x => x.MorningOpening)
                .WithMessage("Morning closing time must be less than or equal to morning opening time.");

            RuleFor(x => x.AfternoonOpening)
                .LessThan(x => x.AfternoonClosing)
                .WithMessage("Afternoon opening time must be greater than or equal to afternoon closing time.");

            RuleFor(x => x.AfternoonClosing)
                .GreaterThanOrEqualTo(x => x.AfternoonOpening)
                .WithMessage("Afternoon closing time must be less than or equal to afternoon opening time.");

            RuleFor(x => x.DaysClosed)
                .Must(x => _validator.ValidateDaysClosedM(x))
                .WithMessage("Invalid days ");
        }
    }
}
