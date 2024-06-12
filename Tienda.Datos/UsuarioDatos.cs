using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Datos
{
    public class UsuarioDatos
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Ingrese su nombre")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "Ingrese su correo")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña")]
        public string? Clave { get; set; }

        [Required(ErrorMessage = "Repita su contraseña")]
        public string? CClave { get; set; }

        public string? Rol { get; set; }


    }
}
