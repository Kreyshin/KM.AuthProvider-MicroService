using AP.Aplicacion.Autenticacion.CasosUso;
using AP.Aplicacion.Autenticacion.Interfaces;
using AP.Aplicacion.Autenticacion.Mappers;
using AP.Aplicacion.Conexiones.CasosUso;
using AP.Aplicacion.Conexiones.Interfaces;
using AP.Aplicacion.Conexiones.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Aplicacion.Configuracion
{
   public static class InyeccionAplicacionEX
    {
        public static IServiceCollection InyeccionAplicacion(this IServiceCollection services)
        {
            services.AddScoped<IAutenticacionCU, AutenticacionCU>();
            services.AddScoped<IGestionConexionesCU, GestionConexionesCU>();

            services.AddAutoMapper(
                typeof(UsuariosProfileAMQ).Assembly,
                typeof(GestionConexionesProfileAM).Assembly
                );

                return services;
        }

    }
}
