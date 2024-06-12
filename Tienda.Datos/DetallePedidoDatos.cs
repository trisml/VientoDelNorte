using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Datos
{
    public class DetallePedidoDatos
    {
        public int IdDetallePedido { get; set; }
        public int? IdProducto { get; set; }
        public string? ProductoNombre { get; set; } 

        public int? Cantidad { get; set; }
        public decimal? Total { get; set; }
    }
}
