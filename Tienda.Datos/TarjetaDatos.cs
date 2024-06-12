using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Datos
{
    public class TarjetaDatos
    {
        [Required(ErrorMessage = "Ingrese su nombre")]
        public string? Titular { get; set; }

        [Required(ErrorMessage = "Ingrese su numero de tarjeta")]
        public string? Numero { get; set; }
        [Required(ErrorMessage = "Ingrese la fecha de caducidad")]
        public string? Caducidad { get; set; }
        [Required(ErrorMessage = "Ingrese su CVV")]
        public string? CVV { get; set; }
    }
}
