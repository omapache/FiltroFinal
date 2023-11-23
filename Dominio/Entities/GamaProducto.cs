using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class GamaProducto : BaseEntityB
    {
        public string DescripcionTexto {get;set;}
        public string DescripcionHtml {get;set;}
        public string Imagen {get;set;}
        public ICollection<Producto> Productos {get;set;}
    }
}