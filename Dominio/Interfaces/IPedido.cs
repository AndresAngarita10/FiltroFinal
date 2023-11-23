
using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IPedido : IGenericRepoInt<Pedido>
{
    public Task<IEnumerable<object>> PedidoNoEntregadoATiempo1();
}
