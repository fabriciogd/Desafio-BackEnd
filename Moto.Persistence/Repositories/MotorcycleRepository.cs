using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Domain.Repositories;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;

namespace Moto.Persistence.Repositories;

public class MotorcycleRepository(MotoDbContext _context) : Repository<Motorcycle>(_context), IMotorcyleRepository
{
    public async Task<bool> ExistWithPlateAsync(string? plate, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(x => x.Placa.ToLower() == plate.ToLower())
            .AnyAsync(cancellationToken);
    }

    public async Task<Motorcycle?> FindByIdentificatorAsync(string? identificator, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(x => x.Identificador.ToLower() == identificator.ToLower())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Motorcycle>> ListByPlateAsync(string? plate, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(x => 
                string.IsNullOrEmpty(plate) ?  true :  x.Placa.ToLower() == plate.ToLower()
            ).ToListAsync(cancellationToken);
    }
}
