using FluentValidation;
using Moto.Domain.ValueObjects;

namespace Moto.Domain.Validators;

public sealed class CnhValidator : AbstractValidator<Cnh>
{
    Func<string, bool> IsValid = (string cnh) =>
    {
        if (string.IsNullOrEmpty(cnh))
            return false;

        var firstChar = cnh[0];
        if (cnh.Length == 11 && cnh != new string('1', 11))
        {
            var dsc = 0;
            var v = 0;
            for (int i = 0, j = 9; i < 9; i++, j--)
            {
                v += (Convert.ToInt32(cnh[i].ToString()) * j);
            }

            var vl1 = v % 11;
            if (vl1 >= 10)
            {
                vl1 = 0;
                dsc = 2;
            }

            v = 0;
            for (int i = 0, j = 1; i < 9; ++i, ++j)
            {
                v += (Convert.ToInt32(cnh[i].ToString()) * j);
            }

            var x = v % 11;
            var vl2 = (x >= 10) ? 0 : x - dsc;

            return vl1.ToString() + vl2.ToString() == cnh.Substring(cnh.Length - 2, 2);
        }

        return false;
    };

    public CnhValidator()
    {
        RuleFor(cnh => cnh.Value)
            .NotEmpty()
            .MinimumLength(11).WithMessage("CNH deve conter o mínimo de 11 caracteres")
            .Must(IsValid)
            .WithMessage("CNH inválido")
            .WithErrorCode("CnhValidator");
    }
}