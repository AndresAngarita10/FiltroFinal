
namespace Dominio.Entities;

public class Producto : BaseEntityStr
{
    public string Nombre { get; set; }
    public string GamaIdFk { get; set; }
    public GamaProducto GamaProducto { get; set; }
    public string Dimensiones { get; set; }
    public string Proveedor { get; set; }
    public string Descripcion { get; set; }
    public short CantidadEnStok { get; set; }
    public decimal PrecioVenta { get; set; }
    public decimal PrecioProveedor { get; set; }
    public ICollection<DetallePedido> DetallePedidos {get;set;}

}
