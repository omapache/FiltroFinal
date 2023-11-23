# Jardineria Filtro Final

Este proyecto proporciona una API que permite gestionar el apartado de la administración de una Jardinearia

## Uso 🕹

Una vez que el proyecto esté en marcha, puedes acceder a los diferentes endpoints disponibles:

## Características 🌟

- CRUD completo para cada entidad.
- Vista de las consultas requeridas.

## Desarrollo de los Endpoints requeridos⌨️

## 1.Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningún pedido

Enpoint:

    http://localhost:5297/api/cliente/consulta1

Codigo Consulta:

    public async Task<IEnumerable<object>> ClientesConCantidadPedidos() // 1
        {
            var clientesConPedidos = await _context.Clientes
                .GroupJoin(
                    _context.Pedidos,
                    cliente => cliente.Id,
                    pedido => pedido.IdClienteFk,
                    (cliente, pedidos) => new
                    {
                        NombreCliente = cliente.NombreCliente,
                        CantidadPedidos = pedidos.Count()
                    }
                )
                .ToListAsync();


            return clientesConPedidos;
        }
Explicacion:

    se llama a la entidad cliente para despues hacer un groupJoin en donde agregare los pedidos luego los clientes que coincidan conel pedido para despues hacer un select con ambos y luego traer la informacion de ambos que en este caso son los clientes y seguidos los pedidos los cuales tiene el metodo .Count para contarlos y mostrar el numero 

## 2. Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo.

Enpoint:

    http://localhost:5297/api/cliente/consulta2

Codigo Consulta:

    public async Task<IEnumerable<object>> PedidoNoAtiempo() // 2
        {
            return await (
                from p in _context.Pedidos
                join c in _context.Clientes on p.IdClienteFk equals c.Id
                
                where p.FechaEsperada != p.FechaEntregada
                select new
                {
                    CodigoPedido = p.Id,
                    CodigoCliente = c.Id,
                    FechaEsperada = p.FechaEsperada,
                    FechaEntregada = p.FechaEntregada
                }
            ).Distinct()
            .ToListAsync();
        }

Explicacion:  

    se llama a la entidad pedido para luego llamar la entidad cliente, despues aplicamos un where con los atributos Fecha Esperada y Fecha Entregada los cuales no deben ser lo mismo para que se cumpla la regla de que la fecha entregada no sea la misma que la esperada y despues hacemos un select donde incluimos la informacion del cliente, pedido y las fechas 

## 3. Devuelve un listado de los productos que nunca han aparecido en un  pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto.

Enpoint:

    http://localhost:5297/api/cliente/consulta3

Codigo Consulta:

    public async Task<IEnumerable<object>> ProductosSinPedidos() // 3
        {
            var productosSinPedidos = await _context.Productos

                .Where(p => !_context.DetallePedidos.Any(a => a.IdProductoFk == p.Id))
                .Select(p => new
                {
                    NombreProducto = p.Nombre
                })
                .ToListAsync();

            return productosSinPedidos;
        }

Explicacion:  

    traemos la entidad de productos para luego aplicarle un where donde le decimos que en detalle pedido no debe de haber un producto por medio de los ids para luego traer el nombre del producto

## 4. Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.

Endpoint:

    http://localhost:5297/api/cliente/consulta4

Codigo Consulta:

    public async Task<IEnumerable<object>> OficinasNoTrabajanEmpleados() // 4
        {
            return await (
                from dt in _context.DetallePedidos
                join pe in _context.Pedidos on dt.IdPedidoFk equals pe.Id
                join c in _context.Clientes on pe.IdClienteFk equals c.Id
                join em in _context.Empleados on c.IdEmpleadoFk equals em.Id
                join of in _context.Oficinas on em.IdOficinaFk equals of.Id
                where dt.Producto.IdGamaProductoFk.ToLower().Contains("Frutales")
                where !em.Puesto.ToLower().Contains("Representante ventas")
                where em.IdOficinaFk == null
                select new
                {
                    Oficina = of.Id,
                    Ciudad = of.Ciudad,
                }
            ).Distinct()
            .ToListAsync();
        }

Explicacion:

    por medio del detalle pedidos traemos las siguientes entidades : Pedido, Cliente, Empleado, Oficina, para que todo coincida con los detalles pedido para luego en el where poner la regla que el puesto del empleado no debe de ser representante ventas, que la gama de producto no sea frutales y que por ultimo el empleado no este asignado a ninguna oficina 

## 5. Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unidades vendidas, total facturado y total facturado con impuestos (21% IVA).

Endpoint:

    http://localhost:5297/api/producto/consulta5

Codigo Consulta:

    public async Task<IEnumerable<object>> VentasProductosMas3000Euros() // 5
        {
            var query = await _context.DetallePedidos
                .Include(dp => dp.Producto)
                .GroupBy(
                    dp => new { dp.IdProductoFk, dp.Producto.Nombre, dp.Producto.PrecioVenta },
                    (key, group) => new
                    {
                        key.IdProductoFk,
                        key.Nombre,
                        key.PrecioVenta,
                        TotalFacturado = group.Sum(dp => (float)dp.Cantidad * dp.Producto.PrecioVenta)
                    })
                .Where(result => result.TotalFacturado * 1.21 > 3000)
                .ToListAsync();
            var info = query
                .Select(item => new
                {
                    NombreProducto = item.Nombre,
                    UnidadesVendidas = _context.DetallePedidos
                        .Where(dp => dp.IdProductoFk == item.IdProductoFk)
                        .Sum(dp => dp.Cantidad),
                    totalFacturadoSinImpuestos = item.TotalFacturado,
                    TotalConImpuestos = item.TotalFacturado * 1.21
                })
                .ToList();
            return info;
        }

Explicacion : 

    esta consulta se divide en 2 bloques, el primer bloque llamado query se encarga de llamar la entidad detalle pedidos para luego incluir los productos, despues se hace un GroupBy el cual estara formado por la entidad producto, el nombre del producto, y el precio de venta del producto para despues mostrar por medio del key el id producto, el nombre, el precio de venta, y el total facturado que se logra sumando la cantidad por el precio de venta de cada producto donde despues se hace el where para aplicarle el iva que es el 1.21 y que tiene que ser mas de 3000 euros  

    en el segundo bloque llamado info se trae la data del bloque de query para hacer el select donde mostraremos en el object la informacion 
## 6.Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos empleados que no sean representante de ventas de ningún cliente.

Enpoint:

    http://localhost:5297/api/empleado/consulta6

Codigo Consulta:

    public async Task<IEnumerable<object>> EmpleadosSinRepresentanteVentas() // 6
            {
                return await (
                    from em in _context.Empleados
                    where !em.Clientes.Any(c => c.IdEmpleadoFk == em.Id && c.Empleado.Puesto.ToLower().Contains("representante ventas"))
                    select new
                    {
                        Nombre = em.Nombre,
                        Apellidos = em.Apellido1 + " " + em.Apellido2,
                        Puesto = em.Puesto,
                        Telefono = em.Oficina.Telefono
                    }
                )
                .ToListAsync();
            }
Explicacion:

    en esta consulta empezamos llamado los empleados y luego aplicandole un filtro que se compone de 2 partes las cuales son que no debe de haber clientes con el id del empleado y que el puesto del empleado no debe de ser representante de ventas para luego traer la informacion de los empleados que si correspondan con el where 
## 7. Devuelve el nombre del producto del que se han vendido más unidades. (Tenga en cuenta que tendrá que calcular cuál es el número total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle_pedido).

Endpoint:

    http://localhost:5297/api/producto/consulta7

Codigo Consulta:

    public async Task<IEnumerable<object>> ProductoMasVendido() // 7
        {
            var query = await _context.DetallePedidos
                .GroupBy(dp => dp.IdProductoFk)
                .Select(g => new
                {
                    CodigoProducto = g.Key,
                    TotalUnidadesVendidas = g.Sum(dp => dp.Cantidad)
                })
                .OrderByDescending(g => g.TotalUnidadesVendidas)
                .FirstOrDefaultAsync();
            var info = await _context.Productos
                .Where(p => p.Id == query.CodigoProducto)
                .Select(p =>
                new
                {
                    NombreProducto = p.Nombre,
                    TotalUnidadesVendidas = query.TotalUnidadesVendidas
                })
                .ToListAsync();
            return info;

        }

Explicacion:

    empezamos llamando la tabla detalles pedidos donde hacer un GroupBy para seguido hacer un select donde elegimos el codigo del producto y el tota de unidades vendidas por medio del metodo sum para despues hacer un order by descending con el total de unidades para elegir despues con el FirstOrDefaultAsync la consulta primera la cual en teoria es la que tiene la mas grande total de unidades vendides para despues con un where agarrar el id del producto y mostrar el nombre con el numero 
## 8. Devuelve un listado de los 20 productos más vendidos y el número total de unidades que se han vendido de cada uno. El listado deberá estar ordenado por el número total de unidades vendidas.

Enpoint: 

    http://localhost:5297/api/producto/consulta8

Codigo Consulta:

    public async Task<IEnumerable<object>> ProductosMasVendidos() // 8
        {
            var query = await (
                from dp in _context.DetallePedidos
                group dp by new { dp.IdProductoFk } into grp
                orderby grp.Sum(dp => dp.Cantidad) descending
                select new
                {
                    IdProducto = grp.Key.IdProductoFk,
                    TotalUnidadesVendidas = grp.Sum(dp => dp.Cantidad)
                }
            ).Take(20).ToListAsync();


            return query;
        }

Explicacion:

    en esta consulta llamamos a detalles pedidos para despues hacer un Group llamado grp donde le hago un orderby descendiente para para asi poder agarrar con el take(20) para con el select mostar la informacion pertinente

## 9. Devuelve el nombre de los clientes a los que no se les ha entregado a tiempo un pedido.

Enpoint:

    http://localhost:5297/api/cliente/consulta9

Codigo Consulta:

    public async Task<IEnumerable<object>> ClienteNoATiempo() // 9
        {
            return await (
                from p in _context.Pedidos
                join c in _context.Clientes on p.IdClienteFk equals c.Id
                
                where p.FechaEsperada != p.FechaEntregada
                select new
                {
                    NombreCliente = c.NombreCliente,
                    FechaEsperada = p.FechaEsperada,
                    FechaEntregada = p.FechaEntregada
                }
            ).Distinct()
            .ToListAsync();
        }

Explicacion:

    llamamos a pedidos y a clientes para despues hacer un where para que me traiga solo los pedidos con los clientes que la fecha esperada no fue la misma de la entregada para luego mostrar los nombres de los clientes 

## 10.  Devuelve un listado de las diferentes gamas de producto que ha comprado cada cliente.

Enpoint:

    http://localhost:5297/api/cliente/consulta10

Codigo Consulta:

    public async Task<IEnumerable<object>> GamaCliente() // 10
            {
                var query = await (
                    from d in _context.DetallePedidos
                    join p in _context.Pedidos on d.IdPedidoFk equals p.Id
                    join c in _context.Clientes on p.IdClienteFk equals c.Id
                    where p.FechaEntregada != p.FechaEsperada
                    select new
                    {
                        CodigoCliente = c.Id,
                        NombreCliente = c.NombreCliente,
                        IdGamaProducto = (
                            from p in _context.Productos
                            where p.Id == d.IdProductoFk
                            select p.IdGamaProductoFk
                        ).ToList()
                    }
                ).ToListAsync();
                var info = query
                    .GroupBy(r => new { r.CodigoCliente, r.NombreCliente })
                    .Select(g => new
                    {
                        CodigoCliente = g.Key.CodigoCliente,
                        NombreCliente = g.Key.NombreCliente,
                        Gamas = g.SelectMany(r => r.IdGamaProducto).Distinct()
                    });
                return info;
            }

Explicacion:

    en esta consulta tambien tenemos 2 bloques, en el bloque de query tenemos las entidades detalle pedidos, pedidos, clientes donde le hacer un where donde la fecha esperada no fue la misma de la entregada para luego en un selec seleccionar la informacion como el codigo del cliente, nombre del cliente y el id gamaproducto
    para luego en el bloque de info con un groupby hacer un grupo donde haremos un select para mostrar la informacion para mostrar el codigo del cliente, nombrel del cliente y las gamas que por medio de la sub consulta del primer select las podemos traer 

## Desarrollo ⌨️
Este proyecto utiliza varias tecnologías y patrones, incluidos:

Entity Framework Core para la ORM.
Patrón Repository y Unit of Work para la gestión de datos.
AutoMapper para el mapeo entre entidades y DTOs.

## Data ✅

la data necesaria para probar la informacion se encuentra en el mismo Proyecto en el archivo llamado "Data.txt"

## Agradecimientos 🎁

A todas las librerías y herramientas utilizadas en este proyecto.

A ti, por considerar el uso de este sistema.

por Owen 🦝

![image](84bcac4692cb6869d70423fd7d25e901.jpg)
