using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Producto : BaseEntityB
    {
        public string Nombre {get;set;}
        public string IdGamaProductoFk {get;set;}
        public GamaProducto GamaProducto {get;set;}
        public string Dimension {get;set;}
        public string Proveedor {get;set;}
        public string Descripcion {get;set;}
        public int CantidadStock {get;set;}
        public float PrecioVenta {get;set;}
        public float PrecioProveedor {get;set;}

        public ICollection<DetallePedido> DetallePedidos {get;set;}
    }
}