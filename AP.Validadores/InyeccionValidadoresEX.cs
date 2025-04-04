using AP.Validadores.Autenticacion;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Validadores
{
    public static class InyeccionValidadoresEX
    {
        public static IServiceCollection InyeccionValidadores(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AutenticacionVL>();
            services.AddValidatorsFromAssembly(typeof(InyeccionValidadoresEX).Assembly);
            services.AddValidatorsFromAssembly(typeof(AutenticacionVL).Assembly);

            return services;
        }
    }
}
