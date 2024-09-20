using Microsoft.EntityFrameworkCore;
using Moto.Application.Interfaces;
using Moto.Domain.Base;
using Moto.Persistence.Contexts;
using System.Data;

namespace Moto.Persistence;

internal sealed class UnitOfWork(MotoDbContext _context) : IUnitOfWork
{
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

                await AfterSaveChangesAsync(domainEvents);
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
            }
        });
    }

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

    private async Task AfterSaveChangesAsync(IReadOnlyList<DomainEvent> domainEvents)
    {

    }
}
