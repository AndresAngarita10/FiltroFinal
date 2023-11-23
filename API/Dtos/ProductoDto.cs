
using Dominio.Entities;

namespace API.Dtos;

public class ProductoDto : BaseEntityStr
{
    public string Nombre { get; set; }
    public string GamaIdFk { get; set; }
    public string Dimensiones { get; set; }
    public string Proveedor { get; set; }
    public string Descripcion { get; set; }
    public short CantidadEnStok { get; set; }
    public decimal PrecioVenta { get; set; }
    public decimal PrecioProveedor { get; set; }

}
