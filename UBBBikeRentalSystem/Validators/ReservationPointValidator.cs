using FluentValidation;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Validators {
    public class ReservationPointValidator : AbstractValidator<ReservationPoint> {
        public ReservationPointValidator() {
            RuleFor(rp => rp.Name)
                .NotEmpty().WithMessage("Pole Name jest wymagane.")
                .MaximumLength(50).WithMessage("Pole Name nie może przekraczać 50 znaków.");

            RuleFor(rp => rp.Address)
                .NotEmpty().WithMessage("Pole Address jest wymagane.")
                .MaximumLength(100).WithMessage("Pole Address nie może przekraczać 100 znaków.");

            RuleFor(rp => rp.City)
                .NotEmpty().WithMessage("Pole City jest wymagane.")
                .MaximumLength(50).WithMessage("Pole City nie może przekraczać 50 znaków.");

            RuleFor(rp => rp.PostalCode)
                .NotEmpty().WithMessage("Pole PostalCode jest wymagane.")
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Pole PostalCode musi być w formacie XX-XXX");

            RuleFor(rp => rp.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Pole Latitude musi być między -90 a 90");

            RuleFor(rp => rp.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Pole Longitude musi być między -180 a 180");
        }
    }
}