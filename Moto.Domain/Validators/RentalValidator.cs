using FluentValidation;
using Moto.Domain.Entities;

namespace Moto.Domain.Validators;

public sealed class RentalValidator : AbstractValidator<Rental>
{
    public RentalValidator()
    {
        RuleFor(rental => rental.PlanId)
            .NotEmpty().WithMessage("Plano deve ser selecionado");

        RuleFor(rental => rental.CourierId)
            .NotEmpty().WithMessage("Entregador deve ser selecionado");

        RuleFor(rental => rental.MotorcycleId)
            .NotEmpty().WithMessage("Moto deve ser selecionada");

        RuleFor(motorcycle => motorcycle.StartDate)
            .NotEmpty().WithMessage("Data de ínicio deve ser fornecida");

        RuleFor(motorcycle => motorcycle.ExpectedEndDate)
            .NotEmpty().WithMessage("Data de ínicio deve ser fornecida");
    }
}