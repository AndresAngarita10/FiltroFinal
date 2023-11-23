
using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IProducto : IGenericRepoStr<Producto>
{
    public Task<IEnumerable<object>> ProductosMasVendidos4();
    public Task<IEnumerable<object>> ProductosMasVendidos5();
    public Task<object> ProductosUnidadesMasVendido6();
}
