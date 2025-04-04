using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Dominio.Comunes
{
    public class ListResponse<T> : RepositoryResult
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
    }
}
