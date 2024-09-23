using FluentValidation;
using Moto.Domain.Entities;

namespace Moto.Domain.Validators;

public sealed class MotorcycleValidator : AbstractValidator<Motorcycle>
{
    public MotorcycleValidator()
    {
        RuleFor(motorcycle => motorcycle.Year)
            .GreaterThanOrEqualTo((short)2020)
            .LessThanOrEqualTo((short)2025);

        RuleFor(motorcycle => motorcycle.Model)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(motorcycle => motorcycle.LicensePlate)
            .NotEmpty()
            .MaximumLength(8)
            .MinimumLength(8)
            .SetValidator(new LicensePlateValidator<Motorcycle>());
    }
}