
using Dominio.Entities;

namespace Dominio.Interfaces;
public interface ICliente : IGenericRepoInt<Cliente>
{
    public Task<IEnumerable<object>> ClientesSinPagos2();
    public Task<IEnumerable<object>> ClientesConSuCantidadDePedidos7();
    public Task<IEnumerable<object>> ClientesConRepresentanteDeVentas8();
}
