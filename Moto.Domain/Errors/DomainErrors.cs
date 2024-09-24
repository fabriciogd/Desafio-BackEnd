namespace Moto.Domain.Errors;

public static class DomainErrors
{
    public static string Invalid => "Dados inválidos";

    public static class Motorcycle
    {
        public static string NotFound => "Moto não encontrada";
        public static string AlreadyExists => "Placa já esta sendo utilizada por outra moto";

        public static string InUse => "Moto já possuí algum contrato de locação";
    }

    public static class Courier
    {
        public static string IncorretImageFormat => "Imagem precisa estar no formato png ou bmp";
        public static string RequiredImage => "Imagem é obrigatória";
        public static string NotFound => "Entregador não encontrada";
    }

    public static class Plan
    {
        public static string NotFound => "Plano não encontrada";
    }

    public static class Rental
    {
        public static string NotFound => "Locação não encontrada";
        public static string StatusFinished => "Locação ja se encontra como finalizada";
    }
}
