using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Datos
{
    public class RespuestaDatos<T>
    {
        public T? Resultado { get; set; }

        public bool Ok { get; set; }

        public string? Mensaje { get; set; }
    }
}
