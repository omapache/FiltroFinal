using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
public class ClienteRepository : GenericRepo<Cliente>, ICliente
{
    protected readonly ApiContext _context;
    public ClienteRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes
            .ToListAsync();
    }
    public override async Task<Cliente> GetByIdAsync(int id)
    {
        return await _context.Clientes
        .FirstOrDefaultAsync(p => p.Id == id);
    }
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
    
}