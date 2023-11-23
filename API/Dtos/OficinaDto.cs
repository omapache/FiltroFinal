using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace API.Dtos
{
    public class OficinaDto : BaseEntityB
    {
        public string Ciudad {get;set;}
        public string Pais {get;set;}
        public string Region {get;set;}
        public string CodigoPostal {get;set;}
        public string Telefono {get;set;}
        public string LineaDireccion1 {get;set;}
        public string LineaDireccion2 {get;set;}
    }
}