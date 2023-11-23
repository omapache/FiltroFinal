using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Pedido : BaseEntity
    {
        public DateOnly FechaPedido {get;set;}
        public DateOnly FechaEsperada {get;set;}
        public DateOnly? FechaEntregada {get;set;}
        public string Estado {get;set;}
        public string Comentarios {get;set;}
        public int IdClienteFk {get;set;}
        public Cliente Cliente {get;set;}

        public ICollection<DetallePedido> DetallePedidos {get;set;}
    }
}