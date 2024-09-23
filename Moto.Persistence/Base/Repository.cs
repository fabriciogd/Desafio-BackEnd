using Microsoft.EntityFrameworkCore;
using Moto.Domain.Base;
using Moto.Domain.Interfaces;
using Moto.Persistence.Contexts;
using System.Linq.Expressions;

namespace Moto.Persistence.Base;

/// <summary>
/// A generic repository that provides common database operations for entities.
/// </summary>
public class Repository<TEntity>(MotoDbContext _context) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet = _context.Set<TEntity>();

    /// <summary>
    /// Asynchronously adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">Token used to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) 
        => await _dbSet.AddAsync(entity, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves an entity by its primary key ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <param name="cancellationToken">Token used to cancel the operation if needed.</param>
    /// <returns>The entity if found, otherwise null.</returns>
    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken) 
        => await GetByIdCompiledAsync(_context, id, cancellationToken);

    /// <summary>
    /// Removes the specified entity from the database.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    public void Remove(TEntity entity) => _dbSet.Remove(entity);

    /// <summary>
    /// Updates the specified entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public void Update(TEntity entity) => _dbSet.Update(entity);

    /// <summary>
    /// Asynchronously retrieves a list of entities that match a given condition.
    /// </summary>
    /// <param name="expression">A LINQ expression that defines the condition to filter the entities.</param>
    /// <param name="cancellationToken">Token used to cancel the operation if needed.</param>
    /// <returns>A list of entities that match the condition.</returns>
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
