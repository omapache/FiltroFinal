using API.Dtos;
using AutoMapper;
using Dominio.Entities;
namespace API.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    { 
       CreateMap<Pago,PagoDto>().ReverseMap();
       CreateMap<Oficina,OficinaDto>().ReverseMap();
       CreateMap<Empleado,EmpleadoDto>().ReverseMap();
       CreateMap<Pedido,PedidoDto>().ReverseMap();
       CreateMap<Cliente,ClienteDto>().ReverseMap();
       CreateMap<Producto,ProductoDto>().ReverseMap();
       CreateMap<GamaProducto,GamaProductoDto>().ReverseMap();
   }
}
