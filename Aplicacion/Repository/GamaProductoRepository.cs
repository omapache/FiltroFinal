using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class GamaProductoRepository  : GenericRepoB<GamaProducto>, IGamaProducto
{
    protected readonly ApiContext _context;
    public GamaProductoRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<GamaProducto>> GetAllAsync()
    {
        return await _context.GamaProductos
            .ToListAsync();
    }
    public override async Task<GamaProducto> GetByIdAsync(string id)
    {
        return await _context.GamaProductos
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }

    
}