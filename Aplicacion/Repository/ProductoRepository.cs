using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
public class ProductoRepository : GenericRepoB<Producto>, IProducto
{
    protected readonly ApiContext _context;
    public ProductoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos
            .ToListAsync();
    }
    public override async Task<Producto> GetByIdAsync(string id)
    {
        return await _context.Productos
        .FirstOrDefaultAsync(p => p.Id == id);
    }
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

}