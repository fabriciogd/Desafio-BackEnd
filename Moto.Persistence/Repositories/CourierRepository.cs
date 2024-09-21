using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Domain.Repositories;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;

namespace Moto.Persistence.Repositories;

public class CourierRepository(MotoDbContext _context) : Repository<Courier>(_context), ICourierRepository
{
    public async Task<Courier?> FindByIdentificadorAsync(string? identificator, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(x => x.Identificador.ToLower() == identificator.ToLower())
            .FirstOrDefaultAsync(cancellationToken);
    }
}
