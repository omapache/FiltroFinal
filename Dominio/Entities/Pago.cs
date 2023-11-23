using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Pago : BaseEntityB
    {
        public int IdClienteFk {get;set;}
        public Cliente Cliente {get;set;}
        public string FormaPago {get;set;}
        public DateOnly FechaPago {get;set;}
        public float Total {get;set;}

    }
}