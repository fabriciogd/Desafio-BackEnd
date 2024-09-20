using Moto.Domain.Entities;
using Moto.Domain.Interfaces;

namespace Moto.Domain.Repositories;

public interface IMotorcyleRepository : IRepository<Motorcycle>
{
    Task<List<Motorcycle>> ListByPlateAsync(string plate, CancellationToken cancellationToken);
}
