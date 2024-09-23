using Moto.Domain.Entities;
using Moto.Domain.Repositories;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;

namespace Moto.Persistence.Repositories;

internal sealed class EventRepository(MotoDbContext _context) : Repository<Event>(_context), IEventRepository;