using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class EmpleadoRepository  : GenericRepo<Empleado>, IEmpleado
{
    protected readonly ApiContext _context;
    public EmpleadoRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Empleado>> GetAllAsync()
    {
        return await _context.Empleados
            .ToListAsync();
    }
    public override async Task<Empleado> GetByIdAsync(int id)
    {
        return await _context.Empleados
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
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
    
    
}