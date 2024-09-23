using FluentValidation;
using Moto.Domain.Entities;

namespace Moto.Domain.Validators;

public sealed class CourierValidator : AbstractValidator<Courier>
{
    public CourierValidator()
    {
        RuleFor(courier => courier.BirthDate)
            .NotEmpty().WithMessage("Data de nascimento não pode ser vazia");

        RuleFor(courier => courier.DrivingLicenseType)
            .NotEmpty().WithMessage("Tipo de CNH não pode ser vazia");
    }
}