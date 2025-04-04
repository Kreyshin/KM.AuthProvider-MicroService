using AP.Aplicacion.Conexiones.Dtos.Request;
using AP.Dominio.Comunes;
using AP.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Aplicacion.Conexiones.Interfaces
{
    public interface IGestionConexionesCU
    {

        public Task<SingleResponse<int>> GuardarConexion(NuevaConexionRQ conexionRQ);

    }
}
