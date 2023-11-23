
using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;
public class UnitOfWork  : IUnitOfWork, IDisposable
{
    private readonly ApiContext _context;

    private ClienteRepository _Clientes;
    private DetallePedidoRepository _DetallePedidos;
    private EmpleadoRepository _Empleados;
    private GamaProductoRepository _GamaProductos;
    private OficinaRepository _Oficina;
    private PagoRepository _Pago;
     private ProductoRepository _productos;
    private PedidoRepository _Pedidos;

    public UnitOfWork(ApiContext context)
    {
        _context = context;
    }
    

    public ICliente Clientes
    {
        get
        {
            if (_Clientes == null)
            {
                _Clientes = new ClienteRepository(_context);
            }
            return _Clientes;
        }
    }
    public IDetallePedido DetallePedidos
    {
        get
        {
            if (_DetallePedidos == null)
            {
                _DetallePedidos = new DetallePedidoRepository(_context);
            }
            return _DetallePedidos;
        }
    }
    
    public IEmpleado Empleados
    {
        get
        {
            if (_Empleados == null)
            {
                _Empleados = new EmpleadoRepository(_context);
            }
            return _Empleados;
        }
    }
    public IGamaProducto GamaProductos
    {
        get
        {
            if (_GamaProductos == null)
            {
                _GamaProductos = new GamaProductoRepository(_context);
            }
            return _GamaProductos;
        }
    }
    public IOficina Oficinas
    {
        get
        {
            if (_Oficina == null)
            {
                _Oficina = new OficinaRepository(_context);
            }
            return _Oficina;
        }
    }
    public IPago Pagos
    {
        get
        {
            if (_Pago == null)
            {
                _Pago = new PagoRepository(_context);
            }
            return _Pago;
        }
    }
    
    public IProducto Productos
    {
        get
        {
            if (_productos == null)
            {
                _productos = new ProductoRepository(_context);
            }
            return _productos;
        }
    }
    public IPedido Pedidos
    {
        get
        {
            if (_Pedidos == null)
            {
                _Pedidos = new PedidoRepository(_context);
            }
            return _Pedidos;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
