using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class DetallePedido 
    {
        public int IdPedidoFk {get;set;}
        public Pedido Pedido {get;set;}
        public string IdProductoFk {get;set;}
        public Producto Producto {get;set;}
        public int Cantidad {get;set;}
        public float PrecioUnidad {get;set;}
        public int NumeroSalida {get;set;}

    }
}