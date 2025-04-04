using AP.Aplicacion.Conexiones.Dtos.Request;
using AP.Dominio.Entidades;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Aplicacion.Conexiones.Mappers
{
    public class GestionConexionesProfileAM : Profile
    {
        public GestionConexionesProfileAM()
        {
            CreateMap<NuevaConexionRQ, ConexionesEN>()
                .ForMember(dest => dest.ID_Usuario, opt => opt.MapFrom(src => src.idUsuario))
                .ForMember(dest => dest.ID_Rol, opt => opt.MapFrom(src => src.idRol))
                .ForMember(dest => dest.C_Cod_Sistema, opt => opt.MapFrom(src => src.codSistema))
                .ForMember(dest => dest.C_RefreshToken, opt => opt.MapFrom(src => src.refreshToken))
                ;
        }
    }
}
