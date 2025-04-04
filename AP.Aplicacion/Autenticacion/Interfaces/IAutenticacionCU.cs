using AP.Aplicacion.Autenticacion.Dtos.Request;
using AP.Aplicacion.Autenticacion.Dtos.Response;
using AP.Dominio.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Aplicacion.Autenticacion.Interfaces
{
    public interface IAutenticacionCU
    {
        public Task<SingleResponse<AutenticacionRS>> Auth(AutenticacionRQ authRQ);
    }
}
