using AP.Dominio.Comunes;
using AP.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Dominio.Interfaces
{
    public interface IConexionRepositoryC
    {
        public Task<SingleResponse<int>> GuardarConexion(ConexionesEN conexiones);
    }
}
