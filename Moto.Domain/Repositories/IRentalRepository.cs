using Moto.Domain.Entities;
using Moto.Domain.Interfaces;

namespace Moto.Domain.Repositories;

public interface IRentalRepository: IRepository<Rental>
{
    Task<bool> ExistsRentalInProgressToMotorcycleAsync(int motorcycleId, CancellationToken cancellationToken);
}