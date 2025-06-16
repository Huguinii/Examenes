using RestAPI.Models.DTOs.UserDto;
using RestAPI.Models.DTOs.UsuarioDTO;
using AutoMapper;
using RestAPI.Models.DTOs.ComentarioDTO;
using RestAPI.Models.Entity;

namespace RestAPI.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            // Ya existentes
            CreateMap<AppUser, UserDto>().ReverseMap();

            CreateMap<ComentarioDTO, ComentarioEntity>().ReverseMap();
            CreateMap<CreateComentarioDTO, ComentarioEntity>().ReverseMap();

            CreateMap<UsuarioDTO, UsuarioEntity>().ReverseMap();
            CreateMap<CreateUsuarioDTO, UsuarioEntity>().ReverseMap();



        }
    }

}
