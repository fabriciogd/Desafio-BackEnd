﻿using Moto.Domain.Entities;
using Moto.Domain.Interfaces;

namespace Moto.Domain.Repositories;

public interface ICourierRepository : IRepository<Courier>
{
    Task<Courier?> FindByIdentificadorAsync(string? identificator, CancellationToken cancellationToken);
}