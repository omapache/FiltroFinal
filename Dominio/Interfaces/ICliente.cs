using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface ICliente : IGenericRepo<Cliente>
    {
        Task<IEnumerable<object>> ClientesConCantidadPedidos(); // 1
        Task<IEnumerable<object>> PedidoNoAtiempo(); // 2
        Task<IEnumerable<object>> ProductosSinPedidos(); // 3
        Task<IEnumerable<object>> OficinasNoTrabajanEmpleados(); // 4
    }
}