using Moto.Domain.Entities;
using Moto.Domain.Interfaces;

namespace Moto.Domain.Repositories;

public interface IMotorcyleRepository : IRepository<Motorcycle>
{
    Task<List<Motorcycle>> ListByPlacaAsync(string? plate, CancellationToken cancellationToken);

    Task<bool> IsPlacaUniqueAsync(string? plate, CancellationToken cancellationToken);

    Task<Motorcycle?> FindByIdentificadorAsync(string? identificator, CancellationToken cancellationToken);
}
