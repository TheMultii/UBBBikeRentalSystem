using FluentValidation;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Validators {
    public class VehicleValidator : AbstractValidator<Vehicle> {
        public VehicleValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa jest wymagana.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model jest wymagany.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Adres URL jest wymagany.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));

            RuleFor(x => x.PricePerHour)
                .NotEmpty().WithMessage("Cena za godzinę jest wymagana.")
                .GreaterThan(0).WithMessage("Cena za godzinę musi być większa od 0.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Opis jest wymagany.");

            RuleFor(x => x.IsAvailable)
                .NotEmpty().WithMessage("Status dostępności jest wymagany.");

            RuleFor(x => x.ManufactureDate)
                .NotEmpty().WithMessage("Data produkcji jest wymagana.");

            RuleFor(x => x.MaxSpeed)
                .GreaterThan(0).When(x => x.MaxSpeed.HasValue).WithMessage("Maksymalna prędkość musi być większa od 0.");
        }
    }
}
