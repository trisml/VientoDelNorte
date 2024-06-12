using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Datos
{
    public class PedidoDatos
    {
        public int IdPedido { get; set; }

        public int? IdUsuario { get; set; }

        public decimal? Total { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public virtual ICollection<DetallePedidoDatos> DetallePedidos { get; set; } = new List<DetallePedidoDatos>();

    }
}
