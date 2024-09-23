using FluentValidation;
using Moto.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace Moto.Domain.Validators;

public sealed class LicensePlateValidator : AbstractValidator<LicensePlate>
{
    Func<string, bool> IsValid = (string value) =>
    {
        if (string.IsNullOrWhiteSpace(value)) { return false; }

        if (value.Length != 8) { return false; }

        value = value.Replace("-", "").Trim();

        if (char.IsLetter(value, 4))
        {
            var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
            return padraoMercosul.IsMatch(value);
        }
        else
        {
            var padraoNormal = new Regex("[a-zA-Z]{3}[0-9]{4}");
            return padraoNormal.IsMatch(value);
        }
    };

    public LicensePlateValidator()
    {
        RuleFor(licensePlate => licensePlate.Value)
            .NotEmpty()
            .MinimumLength(8)
            .Must(IsValid)
            .WithMessage("Placa está no formato incorreto")
            .WithErrorCode("LicensePlateValidator");
    }
}

