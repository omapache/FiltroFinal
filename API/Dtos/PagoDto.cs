using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace API.Dtos
{
    public class PagoDto : BaseEntityB
    {
        public int IdClienteFk {get;set;}
        public ClienteDto ClienteDto {get;set;}
        public string FormaPago {get;set;}
        public DateOnly FechaPago {get;set;}
        public float Total {get;set;}
    }
}