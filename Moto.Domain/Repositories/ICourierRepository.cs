using Moto.Domain.Entities;
using Moto.Domain.Interfaces;

namespace Moto.Domain.Repositories;

public interface ICourierRepository : IRepository<Courier>
{
    Task<bool> ExistsByCnpjAsync(string cnpj, CancellationToken cancellationToken);
    Task<bool> ExistsByDrivingLicenseAsync(string drivingLicense, CancellationToken cancellationToken);
}
