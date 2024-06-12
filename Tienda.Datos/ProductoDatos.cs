using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Datos
{
    public class ProductoDatos
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "Ingrese su nombre")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese su descripción")]
        public string? Descripcion { get; set; }


        public int? IdCategoria { get; set; }

        [Required(ErrorMessage = "Ingrese su precio")]
        public decimal? Precio { get; set; }

        [Required(ErrorMessage = "Ingrese el precio de oferta")]
        public decimal? PrecioOferta { get; set; }

        [Required(ErrorMessage = "Ingrese la cantidad")]
        public int? Cantidad { get; set; }

        [Required(ErrorMessage = "Ingrese la ruta de la imagen")]
        public string? Imagen { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public virtual CategoriaDatos? IdCategoriaNavigation { get; set; }
    }
}
