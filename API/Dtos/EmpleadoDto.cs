using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace API.Dtos
{
    public class EmpleadoDto : BaseEntity
    {
        public string Nombre {get;set;}
        public string Apellido1 {get;set;}
        public string Apellido2 {get;set;}
        public string Extension {get;set;}
        public string Email {get;set;}
        public string IdOficinaFk {get;set;}
        public OficinaDto OficinaDto {get;set;}
        public int? IdJefeFk {get;set;}
        public EmpleadoDto JefeDto {get;set;}
        public string Puesto {get;set;}
    }
}