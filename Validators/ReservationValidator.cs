using FluentValidation;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Validators {
    public class ReservationValidator : AbstractValidator<Reservation> {
        public ReservationValidator() {
            RuleFor(r => r.ReservationDate)
                .NotEmpty().WithMessage("Data rezerwacji jest wymagana.")
                .Must((reservation, reservationDate) => reservationDate < reservation.ReturnDate)
                .WithMessage("Data wypożyczenia musi być wcześniejsza niż data zwrotu.");

            RuleFor(r => r.ReservationPointID)
                .NotEmpty().When(r => r.ReturnPointID != null)
                .WithMessage("Pole Punkt Wypożyczenia jest wymagane, jeśli pole Punkt Zwrotu jest wypełnione.");

            RuleFor(r => r.ReservationDate)
                .NotEmpty().When(r => r.ReservationPointID != null)
                .WithMessage("Data wypożyczenia jest wymagana, jeśli pole Punkt wypożyczenia jest wypełnione.");

            RuleFor(r => r.ReturnDate)
                .NotEmpty().When(r => r.ReturnPointID != null)
                .WithMessage("Data zwrotu jest wymagana, jeśli pole Punkt Zwrotu jest wypełnione.");

            RuleFor(r => r.ReservationDate)
                .NotEmpty().When(r => r.ReservationDate > new DateTime(2100, 12, 31, 23, 59, 59) || r.ReservationDate < new DateTime(2000, 1, 1, 0, 0, 0))
                .WithMessage("Data wypożyczenia nie jest z przedziału 2000-2100.");

            RuleFor(r => r.ReturnDate)
                .NotEmpty().When(r => r.ReturnDate > new DateTime(2100, 12, 31, 23, 59, 59) || r.ReturnDate < new DateTime(2000, 1, 1, 0, 0, 0))
                .WithMessage("Data zwrotu nie jest z przedziału 2000-2100.");
        }
    }
}