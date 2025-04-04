using AP.Aplicacion.Autenticacion.Dtos.Request;
using AP.Aplicacion.Autenticacion.Dtos.Response;
using AP.Dominio.Entidades;
using AP.Dominio.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Aplicacion.Autenticacion.Mappers
{
    public class UsuariosProfileAMQ : Profile
    {
        public UsuariosProfileAMQ()
        {
            CreateMap<AutenticacionRQ, UsuariosEN>()
                 .ForMember(dest => dest.C_Usuario, opt => opt.MapFrom(src => src.usuario))
                .ForMember(dest => dest.C_Clave, opt => opt.MapFrom(src => src.clave))
                .ForMember(dest => dest.C_Cod_Sistema, opt => opt.MapFrom(src => src.cod_sistema));


            CreateMap<UsuariosEN, AutenticacionRS>()
                 .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.ID_Usuario))
                 .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.C_Usuario))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.C_Nombre));
        }
    }
}
