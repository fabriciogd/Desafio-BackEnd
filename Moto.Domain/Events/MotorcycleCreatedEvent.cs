using Moto.Domain.Base;

namespace Moto.Domain.Events
{
    public class MotorcycleCreatedEvent: DomainEvent
    {
        public string? Identificador { get; protected set; }
        public short Ano { get; protected set; }
        public string? Modelo { get; protected set; }
        public string? Placa { get; protected set; }

        public MotorcycleCreatedEvent(string? identificador, short ano, string? modelo, string? placa)
        {
            Identificador = identificador;
            Ano = ano;
            Modelo = modelo;
            Placa = placa;
        }
    }
}
