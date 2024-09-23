using MediatR;
using Microsoft.EntityFrameworkCore;
using Moto.Application.Interfaces;
using Moto.Domain.Base;
using Moto.Persistence.Contexts;
using System.Data;
using System.Threading;

namespace Moto.Persistence;

/// <summary>
/// The UnitOfWork class implements the IUnitOfWork interface and provides a mechanism 
/// to coordinate database operations and domain events within a transaction.
/// It ensures that changes to the database and the publishing of domain events are handled atomically.
/// </summary>
internal sealed class UnitOfWork(MotoDbContext _context, IMediator mediator) : IUnitOfWork
{
    /// <summary>
    /// Saves changes to the database within a transaction and publishes domain events after the commit.
    /// This method uses an execution strategy to handle potential transient failures.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation if needed.</param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                var domainEvents = BeforeSaveChanges();

                var rowsAffected = await _context.SaveChangesAsync();

                await transaction.CommitAsync(cancellationToken);

                await AfterSaveChangesAsync(domainEvents, cancellationToken);
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
            }
        });
    }

    #region Private

    private IReadOnlyList<DomainEvent> BeforeSaveChanges()
    {
        var domainEntities = _context
            .ChangeTracker
            .Entries<BaseEntity>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(entry => entry.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(entry => entry.Entity.ClearDomainEvents());

        return domainEvents.AsReadOnly();
    }

    private async Task AfterSaveChangesAsync(IReadOnlyList<DomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        IEnumerable<Task> tasks = domainEvents.Select(domainEvent => mediator.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(tasks);
    }

    #endregion
}