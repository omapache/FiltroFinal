using System.Reflection;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;
public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions options) : base(options)
    { }
   public DbSet<Pago> Pagos { get; set; }
   public DbSet<Oficina> Oficinas { get; set; }
   public DbSet<Empleado> Empleados { get; set; }
   public DbSet<Pedido> Pedidos { get; set; }
   public DbSet<Cliente> Clientes { get; set; }
   public DbSet<DetallePedido> DetallePedidos { get; set; }
   public DbSet<Producto> Productos { get; set; }
   public DbSet<GamaProducto> GamaProductos { get; set; }
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
   base.OnModelCreating(modelBuilder);

   modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}

}

