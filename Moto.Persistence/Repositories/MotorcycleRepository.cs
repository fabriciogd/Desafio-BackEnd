using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Domain.Repositories;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;

namespace Moto.Persistence.Repositories;

internal sealed class MotorcycleRepository(MotoDbContext _context) : Repository<Motorcycle>(_context), IMotorcyleRepository
{
    public async Task<bool> IsPlacaUniqueAsync(string? plate, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(x => x.Placa.ToLower() == plate.ToLower())
            .AsNoTracking()
            .AnyAsync(cancellationToken);
    }

    public async Task<Motorcycle?> FindByIdentificadorAsync(string? identificator, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(x => x.Identificador.ToLower() == identificator.ToLower())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Motorcycle>> ListByPlacaAsync(string? plate, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(x => 
                string.IsNullOrEmpty(plate) ?  true :  x.Placa.ToLower() == plate.ToLower()
            ).ToListAsync(cancellationToken);
    }
}
