using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Domain.Repositories;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;

namespace Moto.Persistence.Repositories;

internal sealed class CourierRepository(MotoDbContext _context) : Repository<Courier>(_context), ICourierRepository;
