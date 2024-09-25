using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Domain.Repositories;
using Moto.Domain.ValueObjects;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;
using System.Threading;

namespace Moto.Persistence.Repositories;

internal sealed class CourierRepository(MotoDbContext _context) : Repository<Courier>(_context), ICourierRepository
{
    public async Task<bool> ExistsByCnpjAsync(string cnpj, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(x => x.Cnpj.Value.ToLower() == cnpj.ToLower())
            .AsNoTracking()
            .AnyAsync(cancellationToken);
    }

    public async  Task<bool> ExistsByDrivingLicenseAsync(string drivingLicense, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(x => x.DrivingLicense.Value.ToLower() == drivingLicense.ToLower())
            .AsNoTracking()
            .AnyAsync(cancellationToken);
    }
}
