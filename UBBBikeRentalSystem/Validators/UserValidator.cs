using FluentValidation;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Validators {
    public class UserValidator : AbstractValidator<User> {
        public UserValidator() {
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("Pole UserName jest wymagane.")
                .Length(3, 50).WithMessage("Pole UserName musi mieć od 3 do 50 znaków.");
        }
    }
}
