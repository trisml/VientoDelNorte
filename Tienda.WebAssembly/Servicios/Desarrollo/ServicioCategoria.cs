using Tienda.Datos;
using Tienda.WebAssembly.Servicios.Interfaces;
using System.Net.Http.Json;

namespace Tienda.WebAssembly.Servicios.Desarrollo
{
    public class ServicioCategoria : IServicioCategoria
    {
        private readonly HttpClient _http;

        public ServicioCategoria(HttpClient http)
        {
            _http = http;
        }

        public async Task<RespuestaDatos<CategoriaDatos>> CrearCategoria(CategoriaDatos categoria)
        {
            var respuesta = await _http.PostAsJsonAsync("GestionCategoria/CrearCategoria", categoria);
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaDatos<CategoriaDatos>>();
            return resultado!;
        }

        public async Task<RespuestaDatos<bool>> ActualizarCategoria(CategoriaDatos categoria)
        {
            var respuesta = await _http.PutAsJsonAsync("GestionCategoria/ActualizarCategoria", categoria);
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaDatos<bool>>();
            return resultado!;
        }

        public async Task<RespuestaDatos<bool>> EliminarCategoria(int id)
        {
            var respuesta = await _http.DeleteFromJsonAsync<RespuestaDatos<bool>>($"GestionCategoria/EliminarCategoria/{id}");
            return respuesta!;
        }

        public async Task<RespuestaDatos<List<CategoriaDatos>>> ListarCategorias(string busqueda)
        {
            var respuesta = await _http.GetFromJsonAsync<RespuestaDatos<List<CategoriaDatos>>>($"GestionCategoria/ListarCategorias/{busqueda}");
            return respuesta!;
        }

        public async Task<RespuestaDatos<CategoriaDatos>> ObtenerCategoria(int id)
        {
            var respuesta = await _http.GetFromJsonAsync<RespuestaDatos<CategoriaDatos>>($"GestionCategoria/ObtenerCategoria/{id}");
            return respuesta!;
        }
    }
}
