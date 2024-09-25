using Moto.Domain.Contracts;
using Moto.Domain.Entities;

namespace Moto.Domain.Repositories;

public interface ICourierRepository : IRepository<Courier>
{
    Task<bool> ExistsByCnpjAsync(string cnpj, CancellationToken cancellationToken);
    Task<bool> ExistsByDrivingLicenseAsync(string drivingLicense, CancellationToken cancellationToken);
}
