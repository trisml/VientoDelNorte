using System;
using System.Collections.Generic;

namespace Tienda.Model;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto{ get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public string? Rol { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
