using Moto.Domain.Contracts;
using Moto.Domain.Entities;

namespace Moto.Domain.Repositories;

public interface IRentalRepository: IRepository<Rental>
{
    Task<bool> ExistsRentalToMotorcycleAsync(int motorcycleId, CancellationToken cancellationToken);
}