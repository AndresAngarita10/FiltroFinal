# Proyecto Filtro Final de C# 
### Este proyecto proporciona una API que permite gestionar la administración de una Jardinearia

# Consulta 1
###  Devuelve un listado con el código de pedido, código de cliente, fecha  esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo.

iniciamos la consulta en la tabla Pedidos la cual contiene la fecha esperada y la fecha de entrega, con el where aplicamos la condicion que la fecha de entrega sea diferente de null y ademas la condicion de que la fecha de entrega sea mayor a la fecha esperada y finalmente retornamos el objeto con los datos pedidos.

    http://localhost:5100/api/pedido/consulta1

- Consulta


        public async Task<IEnumerable<object>> PedidoNoEntregadoATiempo1()
            {
                return await _context.Pedidos
                    .Where(c => c.FechaEntrega != null && c.FechaEntrega > c.FechaEsperada )
                    .Select(Pedido => new
                    {
                        Pedido.Id,
                        Pedido.Cliente.NombreCliente,
                        Pedido.FechaEntrega,
                        Pedido.FechaEsperada
                    })
                    .ToListAsync();
            }

# Consulta 2
###  Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.

Iniciamos la consulta en la tabla clientes y aplicamos el fltro que nos valida si en la tabla pagos no hay alguna llave del cliente en algun registro y si el cliente consultado no tiene esta llave aplica para ser retornado en esta consulta, que con la clausula .Select() escogemos que data se va a retornar.

    http://localhost:5100/api/cliente/consulta2

- Consulta


        public async Task<IEnumerable<object>> ClientesSinPagos2()
        {
            return await _context.Clientes
                .Where(c => !c.Pagos.Any())
                .Select(c => new 
                {
                    NombreCliente = c.NombreCliente,
                    NombreRepresentante = c.Empleado.Nombre,
                    OficinaRepresentante = c.Empleado.Oficina.Ciudad
                }).ToListAsync();
        }
        
# Consulta 3
###   Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.

Esta Consulta comienza en la tabla empleado, aplicamos el primer filtro que nos idica si tiene clientes y ademas internamente verificamos que el cliente tenga pedidos usando el operador Any(), seguimos dentro del segundo Any para ingresar a los detalles del pedido para asi con otro Any verificamos que existe en esos detalles de pedido algun producto con la GamaProducto que sea igual a "Frutales", aplicamos un segundo filtro que nos dice si tiene algun codigo de oficina ligado al empleado.

    http://localhost:5100/api/empleado/consulta3

- Consulta


        public async Task<IEnumerable<object>> EmpleadosSinOficinaConClientesCompraronFrutales3()
        {
            var oficinas = await _context.Empleados
               .Where(e => e.CodigoOficina == null)
               .Where(e => e.Clientes.Any(c => c.Pedidos.Any(p => p.DetallePedidos.Any(d => d.Producto.GamaProducto.Id.Equals("Frutales")))))
               .Select(empleado => new
               {
                   oficina = new
                   {
                       empleado.Oficina.Id,
                   }
               }).ToListAsync();
    
            return oficinas;
        }
  
# Consulta 4
###   Devuelve un listado de los 20 productos más vendidos y el número total de unidades que se han vendido de cada uno. El listado deberá estar ordenado por el número total de unidades vendidas.

En esta consulta comenzamos con la tabla DetallePedido en la cual hacemos una agrupacion con la llave foranea lalamda CodigoProducto, al hacer esta agrupacion nos genera un solo listado sin repetir las llaves foraneas las cuales se usaran para consultar el en comando select lo que es el nombre del producto y asi mismo a traves de la agrupacion que se hizo anteriormente calculamos la cantidad de los productos vendidos usando la propiedad sum y accediendo a la otra propiedad de la tabla detalle que nos dice la cantidad de cada uno

    http://localhost:5100/api/producto/consulta4

- Consulta


        public async Task<IEnumerable<object>> ProductosMasVendidos4()
        {
            return await _context.DetallePedidos
                .GroupBy(detalle => detalle.CodigoProducto)
                .Select(producto => new
                {
                    CodigoProducto = producto.Key,
                    NombreProducto = producto.First().Producto.Nombre,
                    TotalUnidadesVendidas = producto.Sum(detalle => detalle.Cantidad)
                })
                .Take(20)
                .OrderByDescending(producto => producto.TotalUnidadesVendidas)
                .ToListAsync();
        }
        
# Consulta 5
###  Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unidades vendidas, total facturado y total facturado con impuestos (21% IVA).

En esta consulta se debe buscar el producto con ventas mayores a 3000, en esta consulta agrupamos en la tabla detalleproducto por sus llaves foraneas llamadas Codigoproductos, depsues hacemos un fuiltro que hace una operacion entre las propiedades de cantidad y precio unidad y asi mismo comparamos que este reultado sea mayor a 3000,
luego con el .Select() armamos el objeto a retornar pero en el ultimo atributo realizamos el calculo del total mas lo impuestos que son pedidos en el enunciado, esta operacion se hace realizando conversiones primeramente a decimal para calcular la cantidad por precio unidas y seguido a esto se opera con el porcentaje que se aumenta que es el 21% y finalizando se retorna los valores como un double.

    http://localhost:5100/api/producto/consulta5

- Consulta


        public async Task<IEnumerable<object>> ProductosMayorVentas5()
        {
            return await _context.DetallePedidos
            .GroupBy(detalle => detalle.CodigoProducto)
            .Where(producto => producto.Sum(detalle => detalle.Cantidad * detalle.PrecioUnidad) > 3000)
            .Select(producto => new
            {
                CodigoP = producto.Key,
                NombreP = producto.First().Producto.Nombre,
                TotalUnVendidas = producto.Sum(detalle => detalle.Cantidad),
                Total = Convert.ToDouble(producto.Sum(detalle => detalle.Cantidad * detalle.PrecioUnidad)),
                ConImpuestos = Convert.ToDouble(producto.Sum(detalle => (Convert.ToDecimal(detalle.Cantidad) * detalle.PrecioUnidad)) * Convert.ToDecimal(1.21))
            }).ToListAsync();
        }
              
# Consulta 6
###  Devuelve el nombre del producto del que se han vendido más unidades. (Tenga en cuenta que tendrá que calcular cuál es el número total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle_pedido)

Esta consulta la comenzamos en la tabla detallepedidos en la cual hacemos una agrupacion por los codigosProducto, armamos el objeto con el select el cual lleva el codigo, el nombre que se toma con la propiedad first y asi accedemos la las demas propiedades del objeto y por ultimo TotalUnVendidas la cual hace el calculo usando la propiedad Sum(). Finalmente ordenamos descendentemente a traves de las unidades vendidad y retornamos el primero que es el que debe tener la mayor cantidad.

    http://localhost:5100/api/producto/consulta6

- Consulta

        public async Task<object> ProductosUnidadesMasVendido6()
            {
                return await _context.DetallePedidos
                .GroupBy(detalle => detalle.CodigoProducto)
                .Select(producto => new
                {
                    CodigoP = producto.Key,
                    NombreP = producto.First().Producto.Nombre,
                    TotalUnVendidas = producto.Sum(detalle => detalle.Cantidad),
                }).OrderByDescending(p => p.TotalUnVendidas).FirstOrDefaultAsync();
            }
                
# Consulta 7
###   Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningún pedido.

Esta conulta comienza en la tabla Clientes, armamos el objeto con un select para retornar el nombre de cada cliente y asi mismo en la propiedad Numpedidos hacemos el conteo de los pedidos existentes en la tabla Pedidos con lapropiedad Count() y luegos los retornamos ascendentemente.

    http://localhost:5100/api/cliente/consulta7

- Consulta

        public async Task<IEnumerable<object>> ClientesConSuCantidadDePedidos7()
        {
            return await _context.Clientes
            .Select(c => new
            {
                c.NombreCliente,
                Numpedidos = c.Pedidos.Count()
            }).OrderBy(c => c.Numpedidos).ToListAsync();
        }
                
# Consulta 8
###    Devuelve el listado de clientes donde aparezca el nombre del cliente, el nombre y primer apellido de su representante de ventas y la ciudad donde está su oficina.

Esta consulta nos pide retornar los clientes junto a sus representantes de ventas y la oficina, entonces comenzamos en la tabla clientes quenos trae todos los clientes y luego armamos el objeto el cual lleva le nombre del cliente, el nombre del representante de ventas y su primer apellido, para retornar la infomracion del represnetante accedemos a las propiedades de empleado que como hicieramos un join nos trae todos sus atributos y con la ciuad de la oficina .

    http://localhost:5100/api/cliente/consulta8

- Consulta

        public async Task<IEnumerable<object>> ClientesConRepresentanteDeVentas8()
        {
            return await _context.Clientes
            .Select(c => new 
            {
                NomBreCliente = c.NombreCliente,
                NombreRepVentas = c.Empleado.Nombre,
                ApellidoRepVentas = c.Empleado.Apellido1,
                CiudadOficina = c.Empleado.Oficina.Ciudad
            }).ToListAsync();
        }
         
# Consulta 9
###     Devuelve un listado con los datos de los empleados que no tienen clientes asociados y el nombre de su jefe asociado.

Esta consulta comienza en la tabla empleados en la cual ahcemos un filtro !e.Clientes.Any() validando que no tenga clientes asociados, luego armamos el  objeto con el Select() que lleva las propuedades de nombre, apellido1, apellido2, extensiony en la ultima propiedad accedemos con linq a la propiedades del jefe que estan referneciadas en la misma tabla y esta nos trae el jefe inmediato.

    http://localhost:5100/api/empleado/consulta9

- Consulta

        public async Task<IEnumerable<object>> EmpleadosSinClientesYsusjefes9()
        {
            return await _context.Empleados
            .Where(e => !e.Clientes.Any())
            .Select(e => new 
            {
                NombreEmpleado = e.Nombre,
                ApelldioEmpleado = e.Apellido1,
                Apelldio2Empleado = e.Apellido2,
                ExtensionEmpleado = e.Extension,
                Jefe = e.Jefe.Nombre
            }).ToListAsync();
        }
        
# Consulta 9
###      Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto.

Esta consulta ocmienzxa en la tabla productos a la cual le hacemos un filtro con la propiedad .Any() la cual nos indica que no tiene pedidos asociados a la tabla detalle pedidos y luego armamos el objeto  con el .Select() el cual retorna el nombre del producto, descripcion y la imagen existente en ese momento.

    http://localhost:5100/api/producto/consulta10

- Consulta

        public async Task<IEnumerable<object>> ProductosSinPedidos10()
        {
            return await _context.Productos
                .Where(p => !p.DetallePedidos.Any())
                .Select(p => new
                {
                    NombreProducto = p.Nombre,
                    Descripcion = p.Descripcion,
                    Imagen = p.GamaProducto.Imagen
                })
                .ToListAsync();
        }
#
# Agradecimientos
###   Gracias por Tener en cuenta y revisar cada una de estas consultas
