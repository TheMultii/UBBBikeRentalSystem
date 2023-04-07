using FluentValidation;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Validators {
    public class VehicleTypeValidator : AbstractValidator<VehicleType> {
        public VehicleTypeValidator() {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Pole Name jest wymagane.");
        }
    }

}