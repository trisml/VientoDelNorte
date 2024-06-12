using Tienda.Datos;
using Tienda.WebAssembly.Servicios.Interfaces;
using System.Net.Http.Json;

namespace Tienda.WebAssembly.Servicios.Desarrollo
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly HttpClient _http;

        // Constructor que inyecta la instancia de HttpClient
        public ServicioUsuario(HttpClient http)
        {
            _http = http;
        }

        // Método para autenticar al usuario
        public async Task<RespuestaDatos<SesionDatos>> AutenticarUsuario(LoginDatos login)
        {
            // Realiza una solicitud POST a la API para autenticar al usuario
            var respuesta = await _http.PostAsJsonAsync("GestionUsuario/Autenticar", login);
            // Lee y devuelve la respuesta de la API como RespuestaDatos<SesionDatos>
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaDatos<SesionDatos>>();
            return resultado!;
        }

        // Método para crear un nuevo usuario
        public async Task<RespuestaDatos<UsuarioDatos>> CrearUsuario(UsuarioDatos usuario)
        {
            // Realiza una solicitud POST a la API para crear un nuevo usuario
            var respuesta = await _http.PostAsJsonAsync("GestionUsuario/Crear", usuario);
            // Lee y devuelve la respuesta de la API como RespuestaDatos<UsuarioDatos>
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaDatos<UsuarioDatos>>();
            return resultado!;
        }

        // Método para actualizar los datos de un usuario
        public async Task<RespuestaDatos<bool>> ActualizarUsuario(UsuarioDatos usuario)
        {
            // Realiza una solicitud PUT a la API para actualizar los datos del usuario
            var respuesta = await _http.PutAsJsonAsync("GestionUsuario/Actualizar", usuario);
            // Lee y devuelve la respuesta de la API como RespuestaDatos<bool>
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaDatos<bool>>();
            return resultado!;
        }

        // Método para eliminar un usuario por su ID
        public async Task<RespuestaDatos<bool>> EliminarUsuario(int id)
        {
            // Realiza una solicitud DELETE a la API para eliminar el usuario
            var respuesta = await _http.DeleteFromJsonAsync<RespuestaDatos<bool>>($"GestionUsuario/Eliminar/{id}");
            // Lee y devuelve la respuesta de la API como RespuestaDatos<bool>
            return respuesta!;
        }

        // Método para listar los usuarios filtrados por rol y búsqueda
        public async Task<RespuestaDatos<List<UsuarioDatos>>> ListarUsuarios(string rol, string busqueda)
        {
            // Realiza una solicitud GET a la API para obtener la lista de usuarios
            var respuesta = await _http.GetFromJsonAsync<RespuestaDatos<List<UsuarioDatos>>>($"GestionUsuario/Listar/{rol}/{busqueda}");
            // Lee y devuelve la respuesta de la API como RespuestaDatos<List<UsuarioDatos>>
            return respuesta!;
        }

        // Método para obtener un usuario por su ID
        public async Task<RespuestaDatos<UsuarioDatos>> ObtenerUsuario(int id)
        {
            // Realiza una solicitud GET a la API para obtener los datos de un usuario
            var respuesta = await _http.GetFromJsonAsync<RespuestaDatos<UsuarioDatos>>($"GestionUsuario/Obtener/{id}");
            // Lee y devuelve la respuesta de la API como RespuestaDatos<UsuarioDatos>
            return respuesta!;
        }
    }
}
