using FluentValidation;
using Moto.Domain.Entities;

namespace Moto.Domain.Validators;

public sealed class MotorcycleValidator : AbstractValidator<Motorcycle>
{
    public MotorcycleValidator()
    {
        RuleFor(motorcycle => motorcycle.Year)
            .GreaterThanOrEqualTo((short)2020).WithMessage("Ano não pode ser menor que 2020")
            .LessThanOrEqualTo((short)2025).WithMessage("Ano não pode ser maior que 2025");

        RuleFor(motorcycle => motorcycle.Model)
            .NotEmpty().WithMessage("Modelo não pode ser vazio")
            .MaximumLength(50).WithMessage("Modelo precisa ter menos de 50 caracteres ");
    }
}