namespace Dominio.Interfaces;
public interface IUnitOfWork
{
   IPago Pagos { get; }
   IOficina Oficinas { get; }
   IEmpleado Empleados { get; }
   IPedido Pedidos { get; }
   ICliente Clientes { get; }
   IProducto Productos { get; }
   IGamaProducto GamaProductos { get; }
Task<int> SaveAsync();
}
