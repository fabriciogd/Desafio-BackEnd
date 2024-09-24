namespace Moto.Application.Interfaces;

/// <summary>
/// Defines a contract for a unit of work pattern, providing methods for saving changes to the data store.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Asynchronously saves changes made in the unit of work to the data store.
    /// </summary>
    /// <param name="cancellationToken">An optional cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
