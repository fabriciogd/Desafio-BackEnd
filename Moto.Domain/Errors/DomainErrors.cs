namespace Moto.Domain.Errors
{
    public static class DomainErrors
    {
        public static string Invalid = "Dados inválidos";

        public static class Motorcycle
        {
            public static string NotFound => "Moto não encontrada";
        }

        public static class Courier
        {
            public static string NotFound => "Entregador não encontrada";
        }
    }
}
