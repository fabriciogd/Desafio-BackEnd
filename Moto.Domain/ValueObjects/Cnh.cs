using Moto.Domain.Base;
using Moto.Domain.Validators;

namespace Moto.Domain.ValueObjects;
public class Cnh : ValueObject
{
    public Cnh(string value)
    {
        Value = value;

        Validate();
    }

    public string? Value { get; private set; }
    public static Cnh Create(string? value) => new(value);

    protected override bool Validate()
    {
        return OnValidate<CnhValidator, Cnh>();
    }

    public static implicit operator string(Cnh cnpj) => cnpj.Value;

    public static implicit operator Cnh(string value) => new(value);
}