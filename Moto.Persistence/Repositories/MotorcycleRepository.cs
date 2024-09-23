using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Domain.Repositories;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;

namespace Moto.Persistence.Repositories;

internal sealed class MotorcycleRepository(MotoDbContext _context) : Repository<Motorcycle>(_context), IMotorcyleRepository
{
    public async Task<bool> IsLicensePlateUniqueAsync(string? licensePlate, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(x => x.LicensePlate.ToLower() == licensePlate.ToLower())
            .AsNoTracking()
            .AnyAsync(cancellationToken);
    }

    public async Task<List<Motorcycle>> ListAllAsync(string? plate, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(x => 
                string.IsNullOrEmpty(plate) ?  true :  x.LicensePlate.ToLower() == plate.ToLower()
            ).ToListAsync(cancellationToken);
    }
}
