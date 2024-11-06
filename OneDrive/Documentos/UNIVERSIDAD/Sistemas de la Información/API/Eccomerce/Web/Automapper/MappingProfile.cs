using ApiSampleFinal.Models.DTO;

using AutoMapper;
using Domain;
using Eccomerce.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiSampleFinal.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


          
            UsuarioMapper();
            ProductoMapper();
            
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

       


    }


}
