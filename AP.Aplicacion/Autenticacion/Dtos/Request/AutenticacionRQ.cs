using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Aplicacion.Autenticacion.Dtos.Request
{
    public class AutenticacionRQ
    {
        public string usuario { set; get; } = String.Empty;
        public string clave { set; get; } = String.Empty;
        public string cod_sistema { set; get; } = String.Empty;
    }
}
