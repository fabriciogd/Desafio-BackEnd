namespace Moto.Domain.Errors
{
    public static class DomainErrors
    {
        public static string Invalid = "Dados inválidos";

        public static class Motorcycle
        {
            public static string NotFound => "Moto não encontrada";
        }
    }
}
