using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;
public class UnitOfWork  : IUnitOfWork, IDisposable
{
   public UnitOfWork(ApiContext context)
   {
       _context = context;
   }
   private readonly ApiContext _context;
   private PagoRepository _Pagos;
   public IPago Pagos
   {
       get{
           if(_Pagos== null)
           {
               _Pagos= new PagoRepository(_context);
           }
           return _Pagos;
       }
   }
   private OficinaRepository _Oficinas;
   public IOficina Oficinas
   {
       get{
           if(_Oficinas== null)
           {
               _Oficinas= new OficinaRepository(_context);
           }
           return _Oficinas;
       }
   }
   private EmpleadoRepository _Empleados;
   public IEmpleado Empleados
   {
       get{
           if(_Empleados== null)
           {
               _Empleados= new EmpleadoRepository(_context);
           }
           return _Empleados;
       }
   }
   private PedidoRepository _Pedidos;
   public IPedido Pedidos
   {
       get{
           if(_Pedidos== null)
           {
               _Pedidos= new PedidoRepository(_context);
           }
           return _Pedidos;
       }
   }
   private ClienteRepository _Clientes;
   public ICliente Clientes
   {
       get{
           if(_Clientes== null)
           {
               _Clientes= new ClienteRepository(_context);
           }
           return _Clientes;
       }
   }
   private ProductoRepository _Productos;
   public IProducto Productos
   {
       get{
           if(_Productos== null)
           {
               _Productos= new ProductoRepository(_context);
           }
           return _Productos;
       }
   }
   private GamaProductoRepository _GamaProductos;
   public IGamaProducto GamaProductos
   {
       get{
           if(_GamaProductos== null)
           {
               _GamaProductos= new GamaProductoRepository(_context);
           }
           return _GamaProductos;
       }
   }
   public void Dispose()
   {
       _context.Dispose();
   }
   public async Task<int> SaveAsync()
   {
       return await _context.SaveChangesAsync();
   } 
   } 