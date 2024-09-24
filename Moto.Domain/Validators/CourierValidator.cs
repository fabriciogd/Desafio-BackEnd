using FluentValidation;
using Moto.Domain.Entities;

namespace Moto.Domain.Validators;

public sealed class CourierValidator : AbstractValidator<Courier>
{
    public CourierValidator()
    {
        RuleFor(courier => courier.BirthDate)
            .NotEmpty().WithMessage("Data de nascimento não pode ser vazia")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today.AddYears(-18)))
                .WithMessage("Necessário ter ao menos 18 anos");

        RuleFor(courier => courier.DrivingLicenseType)
            .NotEmpty().WithMessage("Tipo de CNH não pode ser vazia");
    }
}