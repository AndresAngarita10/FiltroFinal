
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class EmpleadoRepository: GenericRepoInt<Empleado>, IEmpleado
{
    protected readonly ApiContext _context;

    public EmpleadoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Empleado>> GetAllAsync()
    {
        return await _context.Empleados
            .ToListAsync();
    }

    public override async Task<Empleado> GetByIdAsync(int id)
    {
        return await _context.Empleados
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<object>> EmpleadosSinOficinaConClientesCompraronFrutales3()
        {
            var oficinas = await _context.Empleados
               .Where(e => e.CodigoOficina == null)
               .Where(e => e.Clientes.Any(c => c.Pedidos.Any(p => p.DetallePedidos.Any(d => d.Producto.GamaProducto.Id.Equals("Frutales")))))
               .Select(empleado => new
               {
                   oficina = new
                   {
                       empleado.Oficina.Id,
                   }
               }).ToListAsync();
    
            return oficinas;
        }
    
}