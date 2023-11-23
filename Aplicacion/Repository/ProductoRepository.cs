
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class ProductoRepository : GenericRepoStr<Producto>, IProducto
{
    protected readonly ApiContext _context;

    public ProductoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos
            .ToListAsync();
    }

    public override async Task<Producto> GetByIdAsync(string id)
    {
        return await _context.Productos
        .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public async Task<IEnumerable<object>> ProductosMasVendidos4()
    {
        return await _context.DetallePedidos
            .GroupBy(detalle => detalle.CodigoProducto)
            .Select(producto => new
            {
                CodigoProducto = producto.Key,
                NombreProducto = producto.First().Producto.Nombre,
                TotalUnidadesVendidas = producto.Sum(detalle => detalle.Cantidad)
            })
            .Take(20)
            .OrderByDescending(producto => producto.TotalUnidadesVendidas)
            .ToListAsync();
    }

    public async Task<IEnumerable<object>> ProductosMayorVentas5()
    {
        return await _context.DetallePedidos
           .GroupBy(detalle => detalle.CodigoProducto)
           .Where(producto => producto.Sum(detalle => detalle.Cantidad * detalle.PrecioUnidad) > 3000)
           .Select(producto => new
           {
               CodigoP = producto.Key,
               NombreP = producto.First().Producto.Nombre,
               TotalUnVendidas = producto.Sum(detalle => detalle.Cantidad),
               Total = Convert.ToDouble(producto.Sum(detalle => detalle.Cantidad * detalle.PrecioUnidad)),
               ConImpuestos = Convert.ToDouble(producto.Sum(detalle => (Convert.ToDecimal(detalle.Cantidad) * detalle.PrecioUnidad)) * Convert.ToDecimal(1.21))
           }).ToListAsync();
    }


    public async Task<object> ProductosUnidadesMasVendido6()
    {
        return await _context.DetallePedidos
           .GroupBy(detalle => detalle.CodigoProducto)
           .Select(producto => new
           {
               CodigoP = producto.Key,
               NombreP = producto.First().Producto.Nombre,
               TotalUnVendidas = producto.Sum(detalle => detalle.Cantidad),
           }).OrderByDescending(p => p.TotalUnVendidas).FirstOrDefaultAsync();
    }


    public async Task<IEnumerable<object>> ProductosSinPedidos10()
    {
        return await _context.Productos
            .Where(p => !p.DetallePedidos.Any())
            .Select(p => new
            {
                NombreProducto = p.Nombre,
                Descripcion = p.Descripcion,
                Imagen = p.GamaProducto.Imagen
            })
            .ToListAsync();
    }


}
