using Moto.Domain.Base;
using Moto.Domain.Validators;

namespace Moto.Domain.ValueObjects;

public class Cnpj : ValueObject
{
    public Cnpj(string value)
    {
        Value = value;

        Validate();
    }

    public string? Value { get; private set; }
    public static Cnpj Create(string? value) => new(value);

    protected override bool Validate()
    {
        return OnValidate<CnpjValidator, Cnpj>();
    }

    public static implicit operator string(Cnpj cnpj) => cnpj.Value;

    public static implicit operator Cnpj(string value) => new(value);
}
