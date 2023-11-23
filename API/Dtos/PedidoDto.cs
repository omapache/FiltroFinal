using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace API.Dtos
{
    public class PedidoDto : BaseEntity
    {
        public DateOnly FechaPedido {get;set;}
        public DateOnly FechaEsperada {get;set;}
        public DateOnly? FechaEntregada {get;set;}
        public string Estado {get;set;}
        public string Comentarios {get;set;}
        public int IdClienteFk {get;set;}
        public ClienteDto ClienteDto {get;set;}
    }
}