
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class PedidoRepository: GenericRepoInt<Pedido>, IPedido
{
    protected readonly ApiContext _context;

    public PedidoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .ToListAsync();
    }

    public override async Task<Pedido> GetByIdAsync(int id)
    {
        return await _context.Pedidos
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<IEnumerable<object>> PedidoNoEntregadoATiempo1()
        {
            return await _context.Pedidos
                .Where(c => c.FechaEntrega != null && c.FechaEntrega > c.FechaEsperada )
                .Select(Pedido => new
                {
                    Pedido.Id,
                    Pedido.Cliente.NombreCliente,
                    Pedido.FechaEntrega,
                    Pedido.FechaEsperada
                })
                .ToListAsync();
        }
    
}