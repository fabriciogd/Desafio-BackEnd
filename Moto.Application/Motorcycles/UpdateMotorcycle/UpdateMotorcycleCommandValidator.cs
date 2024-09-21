using FluentValidation;
using Moto.Application.Validators;

namespace Moto.Application.Motorcycles.UpdateMotorcycle;

public sealed class UpdateMotorcycleCommandValidator : AbstractValidator<UpdateMotorcycleCommand>
{
    public UpdateMotorcycleCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.Placa)
            .NotEmpty()
            .MaximumLength(8)
            .MinimumLength(8)
            .SetValidator(new LicensePlateValidator<UpdateMotorcycleCommand>());
    }
}