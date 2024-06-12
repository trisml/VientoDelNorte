using System;
using System.Collections.Generic;

namespace Tienda.Model;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int? IdUsuario { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
