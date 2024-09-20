using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Domain.Repositories;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;

namespace Moto.Persistence.Repositories;

public class MotorcycleRepository(MotoDbContext _context) : Repository<Motorcycle>(_context), IMotorcyleRepository
{
    public async Task<List<Motorcycle>> ListByPlateAsync(string plate, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(x => 
                string.IsNullOrEmpty(plate) ?  true :  x.Placa.ToLower() == plate.ToLower()
            ).ToListAsync(cancellationToken);
    }
}
