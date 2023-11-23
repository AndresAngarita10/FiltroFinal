
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class PagoRepository : GenericRepoStr<Pago>, IPago
{
    protected readonly ApiContext _context;

    public PagoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Pago>> GetAllAsync()
    {
        return await _context.Pagos
            .ToListAsync();
    }

    public override async Task<Pago> GetByIdAsync(string id)
    {
        return await _context.Pagos
        .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }
    
}