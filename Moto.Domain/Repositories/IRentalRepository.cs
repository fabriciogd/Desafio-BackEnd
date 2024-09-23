using Moto.Domain.Entities;
using Moto.Domain.Interfaces;

namespace Moto.Domain.Repositories;

public interface IRentalRepository: IRepository<Rental>
{
    Task<bool> ExistsRentalToMotorcycleAsync(int motorcycleId, CancellationToken cancellationToken);
}