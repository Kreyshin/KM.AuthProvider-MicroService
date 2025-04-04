using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Dominio.Comunes
{
    public class SingleResponse<T> : RepositoryResult
    {
        public T? Data { get; set; }
    }
}
