using Blazored.LocalStorage;
using Tienda.Datos;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Tienda.WebAssembly.Extensiones
{
    public class AutenticacionExt : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        private ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity());

        public AutenticacionExt(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        // Actualiza el estado de autenticación del usuario
        public async Task EstadoAut(SesionDatos usuario)
        {
            ClaimsPrincipal claimsPrin;

            if (usuario != null)
            {
                // Crear ClaimsPrincipal con la información del usuario autenticado
                claimsPrin = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.Rol),
                }, "JwtAuth"));

                // Guardar información de la sesión en el almacenamiento local
                await _localStorage.SetItemAsync("sesionUsuario", usuario);
            }
            else
            {
                // Crear un ClaimsPrincipal vacío para el usuario no autenticado
                claimsPrin = principal;

                // Eliminar información de la sesión del almacenamiento local
                await _localStorage.RemoveItemAsync("sesionUsuario");
            }

            // Notificar el cambio de estado de autenticación
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrin)));
        }

        // Obtiene el estado de autenticación actual
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Obtener la información del usuario de la sesión desde el almacenamiento local
            var usuario = await _localStorage.GetItemAsync<SesionDatos>("sesionUsuario");

            if (usuario == null)
            {
                // Retornar un estado de autenticación vacío si no hay usuario en la sesión
                return await Task.FromResult(new AuthenticationState(principal));
            }

            // Crear ClaimsPrincipal con la información del usuario autenticado
            var claimsPrin = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol),
            }, "JwtAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrin));
        }
    }
}
