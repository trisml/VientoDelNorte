using System;
using System.Collections.Generic;

namespace Tienda.Model;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? IdCategoria { get; set; }

    public decimal? Precio { get; set; }

    public decimal? PrecioOferta { get; set; }

    public int? Cantidad { get; set; }

    public string? Imagen { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }
}
