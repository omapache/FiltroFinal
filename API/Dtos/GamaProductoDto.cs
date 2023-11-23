using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace API.Dtos
{
    public class GamaProductoDto : BaseEntityB
    {
        public string DescripcionTexto {get;set;}
        public string DescripcionHtml {get;set;}
        public string Imagen {get;set;}
    }
}