using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Infraestructura.Persistencia
{
    public interface IDbConfiguracion
    {
       public string ConnectionString { get; }
    }
}
