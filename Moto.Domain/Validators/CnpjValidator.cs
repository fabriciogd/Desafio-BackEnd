using FluentValidation;
using Moto.Domain.ValueObjects;

namespace Moto.Domain.Validators;

public sealed class CnpjValidator : AbstractValidator<Cnpj>
{
    Func<string, bool> IsValid = (string cnpj) =>
    {
        int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
        if (cnpj.Length != 14)
            return false;

        string tempCnpj = cnpj.Substring(0, 12);
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        int resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cnpj.EndsWith(digito);
    };

    public CnpjValidator()
    {
        RuleFor(cnpj => cnpj.Value)
            .NotEmpty()
            .Length(14).WithMessage("CNPJ deve conter 14 caracteres")
            .Must(IsValid)
            .WithMessage("CNPJ inválido")
            .WithErrorCode("CnpjValidator");
    }
}