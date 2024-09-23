using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Domain.Enums;
using Moto.Domain.Repositories;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;

namespace Moto.Persistence.Repositories;

internal sealed class RentalRepository(MotoDbContext _context) : Repository<Rental>(_context), IRentalRepository
{
    public async Task<bool> ExistsRentalToMotorcycleAsync(int motorcycleId, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => 
            x.Status == RentStatusEnum.InProgress && 
            x.MotorcycleId == motorcycleId
        ).AnyAsync();
    }
}
