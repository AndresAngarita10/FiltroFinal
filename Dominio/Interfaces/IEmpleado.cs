
using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IEmpleado : IGenericRepoInt<Empleado>
{
    public Task<IEnumerable<object>> EmpleadosSinOficinaConClientesCompraronFrutales3();
    public Task<IEnumerable<object>> EmpleadosSinClientesYsusjefes9();
}
