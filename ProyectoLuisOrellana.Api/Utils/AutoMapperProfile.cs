using AutoMapper;
using DatabaseFirst.Models;
using ProyectoLuisOrellana.Api.Models;

namespace ProyectoLuisOrellana.Api.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           
            CreateMap<LoginModelDto, User>();
            CreateMap<IntentoDto, Intento>();
        }
    }
}
