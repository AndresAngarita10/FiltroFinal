
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class ClienteRepository : GenericRepoInt<Cliente>, ICliente
{
    protected readonly ApiContext _context;

    public ClienteRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes
            .ToListAsync();
    }

    public override async Task<Cliente> GetByIdAsync(int id)
    {
        return await _context.Clientes
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<object>> ClientesSinPagos2()
    {
        return await _context.Clientes
            .Where(c => !c.Pagos.Any())
            .Select(c => new
            {
                NombreCliente = c.NombreCliente,
                NombreRepresentante = c.Empleado.Nombre,
                OficinaRepresentante = c.Empleado.Oficina.Ciudad
            }).ToListAsync();
    }

    public async Task<IEnumerable<object>> ClientesConSuCantidadDePedidos7()
    {
        return await _context.Clientes
        .Select(c => new
        {
            c.NombreCliente,
            pedido = c.Pedidos.Count()
        }).OrderBy(c => c.pedido).ToListAsync();
    }

    public async Task<IEnumerable<object>> ClientesConRepresentanteDeVentas8()
    {
        return await _context.Clientes
        .Select(c => new 
        {
            NomBreCliente = c.NombreCliente,
            NombreRepVentas = c.Empleado.Nombre,
            ApellidoRepVentas = c.Empleado.Apellido1,
            CiudadOficina = c.Empleado.Oficina.Ciudad
        }).ToListAsync();
    }

}