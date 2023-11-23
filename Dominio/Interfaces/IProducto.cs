using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IProducto : IGenericRepoB<Producto>
    {
        Task<IEnumerable<object>> VentasProductosMas3000Euros(); // 5
        Task<IEnumerable<object>> ProductoMasVendido(); // 7
    }
}