using FluentValidation;
using Moto.Application.Validators;

namespace Moto.Application.Motorcycles.CreateMotorcycle;

public sealed class CreateMotorcycleCommandValidator : AbstractValidator<CreateMotorcycleCommand>
{
    public CreateMotorcycleCommandValidator()
    {
        RuleFor(command => command.Identificador)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(command => command.Ano)
            .GreaterThanOrEqualTo((short)2020)
            .LessThanOrEqualTo((short)2024);

        RuleFor(command => command.Modelo)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(command => command.Placa)
            .NotEmpty()
            .MaximumLength(8)
            .MinimumLength(8)
            .SetValidator(new LicensePlateValidator<CreateMotorcycleCommand>());

    }
}
