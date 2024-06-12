using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Datos
{
    public class CarritoDatos
    {
        public ProductoDatos? Producto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Precio { get; set; }

        public decimal? PrecioTotal { get; set; }
    }
}
