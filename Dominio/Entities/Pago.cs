
namespace Dominio.Entities;

public class Pago : BaseEntityStr
{
    public string FormaPAgo { get; set; }
    public DateTime FechaPAgo { get; set; }
    public decimal Total { get; set; }
    public int CodigoCliente { get; set; }
    public Cliente Cliente { get; set; }
}
