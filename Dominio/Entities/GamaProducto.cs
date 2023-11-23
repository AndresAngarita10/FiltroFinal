
namespace Dominio.Entities;

public class GamaProducto : BaseEntityStr
{
    public string DescripcionTexto { get; set; }
    public string DescripcioHtml { get; set; }
    public string Imagen { get; set; }
    public ICollection<Producto> Productos { get; set; }
}