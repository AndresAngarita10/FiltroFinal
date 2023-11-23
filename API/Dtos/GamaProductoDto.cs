
using Dominio.Entities;

namespace API.Dtos;

public class GamaProductoDto : BaseEntityStr
{
    public string DescripcionTexto { get; set; }
    public string DescripcioHtml { get; set; }
    public string Imagen { get; set; }
}