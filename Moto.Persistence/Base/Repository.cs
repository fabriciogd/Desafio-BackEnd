using Microsoft.EntityFrameworkCore;
using Moto.Domain.Base;
using Moto.Domain.Interfaces;
using Moto.Persistence.Contexts;
using System.Linq.Expressions;

namespace Moto.Persistence.Base;

public class Repository<TEntity>(MotoDbContext _context) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet = _context.Set<TEntity>();

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) 
        => await _dbSet.AddAsync(entity, cancellationToken);

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken) 
        => await GetByIdCompiledAsync(_context, id, cancellationToken);

    public void Remove(TEntity entity) => _dbSet.Remove(entity);

    public void Update(TEntity entity) => _dbSet.Update(entity);

    public Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        => _dbSet.Where(expression).ToListAsync(cancellationToken);

    #region Private

    private static readonly Func<MotoDbContext, int, CancellationToken, Task<TEntity?>> GetByIdCompiledAsync =
        EF.CompileAsyncQuery((MotoDbContext context, int id, CancellationToken cancellationToken) =>
            context
                .Set<TEntity>()
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefault(entity => entity.Id.Equals(id)));

    #endregion
}
