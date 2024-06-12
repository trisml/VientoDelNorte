using System.Threading.Tasks;
using System.Collections.Generic;
using Tienda.Datos;

namespace Tienda.WebAssembly.Servicios.Interfaces
{
    public interface IServicioUsuario
    {
        Task<RespuestaDatos<UsuarioDatos>> CrearUsuario(UsuarioDatos usuario);

        Task<RespuestaDatos<List<UsuarioDatos>>> ListarUsuarios(string rol, string busqueda);

        Task<RespuestaDatos<UsuarioDatos>> ObtenerUsuario(int id);

        Task<RespuestaDatos<SesionDatos>> AutenticarUsuario(LoginDatos login);

        Task<RespuestaDatos<bool>> ActualizarUsuario(UsuarioDatos usuario);

        Task<RespuestaDatos<bool>> EliminarUsuario(int id);
    }
}
