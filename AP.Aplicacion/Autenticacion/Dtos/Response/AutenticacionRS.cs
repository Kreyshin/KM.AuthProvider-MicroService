using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Aplicacion.Autenticacion.Dtos.Response
{
    public class AutenticacionRS
    {
        public int IdUsuario { set; get; }
        public int IdRol { set; get; }
        public string Usuario { set; get; }
        public string Nombre { set; get; }
    }
}
