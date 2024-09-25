using Moto.Domain.Base;
using System.Linq.Expressions;

namespace Moto.Domain.Contracts;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
}