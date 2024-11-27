using ApiSampleFinal.Models.DTO;
using Application.DTOs;
using AutoMapper;
using Domain;
using Eccomerce.Models.DTO;
using Services.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiSampleFinal.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UsuarioMapper();
            ProductoMapper();
            categoriaMapper();
            LoginMapper();
            ProveedorMapper();
            InventarioMapper();
            PagoMapper();
            PedidoMapper();

        }

        private void UsuarioMapper()
        {
            CreateMap<Usuario, UsuarioDTO>()
                .ReverseMap();
        }

        private void ProductoMapper()
        {
            CreateMap<Producto, ProductoDTO>().ReverseMap();
        }

        private void categoriaMapper()
        {

            CreateMap<Categoria, CategoriaDTO>().ReverseMap();

        }

        private void LoginMapper()
        {
            CreateMap<Usuario, LoginRequest>().ReverseMap();
        }
        private void ProveedorMapper()
        {

            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();  
        }

        private void InventarioMapper()
        {

            CreateMap<Inventario, InventarioDTO>().ReverseMap();
        }

        private void PagoMapper()
        {
            CreateMap<Pago, PagoDTO>().ReverseMap();

        }

        private void PedidoMapper()
        {

            CreateMap<Pedido, PedidoDTO>().ReverseMap();
        }


    }


}
