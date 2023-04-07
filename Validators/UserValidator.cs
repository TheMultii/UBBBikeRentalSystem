using FluentValidation;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Validators {
    public class UserValidator : AbstractValidator<User> {
        public UserValidator() {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Pole Name jest wymagane.")
                .Length(3, 50).WithMessage("Pole Name musi mieć od 3 do 50 znaków.");
        }
    }
}
