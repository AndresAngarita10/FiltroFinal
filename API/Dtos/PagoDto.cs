
using Dominio.Entities;

namespace API.Dtos;

public class PagoDto : BaseEntityStr
{
    public string FormaPAgo { get; set; }
    public DateTime FechaPAgo { get; set; }
    public decimal Total { get; set; }
    public int CodigoCliente { get; set; }
}