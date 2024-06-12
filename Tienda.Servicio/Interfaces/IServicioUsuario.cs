using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.Datos;

namespace Tienda.Servicio.Interfaces
{
    public interface IServicioUsuario
    {
        Task<List<UsuarioDatos>> ListarUsuarios(string rol, string busqueda);

        Task<UsuarioDatos> ObtenerUsuario(int id);

        Task<SesionDatos> AutenticarUsuario(LoginDatos login);

        Task<UsuarioDatos> CrearUsuario(UsuarioDatos usuario);

        Task<bool> ActualizarUsuario(UsuarioDatos usuario);

        Task<bool> EliminarUsuario(int id);
    }
}
