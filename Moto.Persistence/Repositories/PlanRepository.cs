using Moto.Domain.Entities;
using Moto.Domain.Repositories;
using Moto.Persistence.Base;
using Moto.Persistence.Contexts;

namespace Moto.Persistence.Repositories;

public class PlanRepository(MotoDbContext _context) : Repository<Plan>(_context), IPlanRepository;
