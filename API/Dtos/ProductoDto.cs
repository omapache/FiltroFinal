using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace API.Dtos
{
    public class ProductoDto : BaseEntityB
    {
        public string Nombre {get;set;}
        public string IdGamaProductoFk {get;set;}
        public GamaProductoDto GamaProductoDto {get;set;}
        public string Dimension {get;set;}
        public string Proveedor {get;set;}
        public string Descripcion {get;set;}
        public int CantidadStock {get;set;}
        public float PrecioVenta {get;set;}
        public float PrecioProveedor {get;set;}
    }
}