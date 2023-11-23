using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Empleado : BaseEntity
    {
        public string Nombre {get;set;}
        public string Apellido1 {get;set;}
        public string Apellido2 {get;set;}
        public string Extension {get;set;}
        public string Email {get;set;}
        public string IdOficinaFk {get;set;}
        public Oficina Oficina {get;set;}
        public int? IdJefeFk {get;set;}
        public Empleado Jefe {get;set;}
        public string Puesto {get;set;}
        

        public ICollection<Cliente> Clientes {get;set;}
        public ICollection<Empleado> Empleados {get;set;}
    }
}