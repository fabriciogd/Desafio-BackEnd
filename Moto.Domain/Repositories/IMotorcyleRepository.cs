using Moto.Domain.Contracts;
using Moto.Domain.Entities;

namespace Moto.Domain.Repositories;

public interface IMotorcyleRepository : IRepository<Motorcycle>
{
    Task<List<Motorcycle>> ListAllAsync(string? plate, CancellationToken cancellationToken);

    Task<bool> ExistsByLicensePlateAsync(string? licensePlate, CancellationToken cancellationToken);
}
