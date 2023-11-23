
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class OficinaRepository : GenericRepoStr<Oficina>, IOficina
{
    protected readonly ApiContext _context;

    public OficinaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Oficina>> GetAllAsync()
    {
        return await _context.Oficinas
            .ToListAsync();
    }

    public override async Task<Oficina> GetByIdAsync(string id)
    {
        return await _context.Oficinas
        .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }
    
}