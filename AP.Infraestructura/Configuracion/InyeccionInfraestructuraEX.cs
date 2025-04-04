using AP.Dominio.Interfaces;
using AP.Infraestructura.Persistencia;
using AP.Infraestructura.Repositorios.Commands;
using AP.Infraestructura.Repositorios.Querys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Infraestructura.Configuracion
{
    public static class InyeccionInfraestructuraEX
    {
        public static IServiceCollection InyeccionInfraestructura(this IServiceCollection services)
        {
            services.AddScoped<DbConexion>();
            services.AddScoped<IUsuariosRepositoryQ, UsuariosRepositoryQ>();
            services.AddScoped<IConexionRepositoryC, ConexionRepositoryC>();
            return services;
        }
    }
}
