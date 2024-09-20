using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Moto.Application.Validators;

public class LicensePlateValidator<T> : IPropertyValidator<T, string>
{
    public string Name => "PlateValidator";

    public bool IsValid(ValidationContext<T> context, string value)
    {
        if (string.IsNullOrWhiteSpace(value)) { return false; }

        if (value.Length > 8) { return false; }

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
    }
    public string GetDefaultMessageTemplate(string errorCode)
      => "'{PropertyName}' não é valida.";
}