using Tienda.Datos;
using Tienda.WebAssembly.Servicios.Interfaces;
using System.Net.Http.Json;

namespace Tienda.WebAssembly.Servicios.Desarrollo
{
    public class ServicioProducto : IServicioProducto
    {
        private readonly HttpClient _http;

        public ServicioProducto(HttpClient http)
        {
            _http = http;
        }

        public async Task<RespuestaDatos<List<ProductoDatos>>> ObtenerCatalogo(string categoria, string busqueda)
        {
            var respuesta = await _http.GetFromJsonAsync<RespuestaDatos<List<ProductoDatos>>>($"GestionProducto/Catalogo/{categoria}/{busqueda}");
            return respuesta!;
        }

        public async Task<RespuestaDatos<ProductoDatos>> CrearProducto(ProductoDatos producto)
        {
            var respuesta = await _http.PostAsJsonAsync("GestionProducto/Crear", producto);
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaDatos<ProductoDatos>>();
            return resultado!;
        }

        public async Task<RespuestaDatos<bool>> ActualizarProducto(ProductoDatos producto)
        {
            var respuesta = await _http.PutAsJsonAsync("GestionProducto/Actualizar", producto);
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaDatos<bool>>();
            return resultado!;
        }

        public async Task<RespuestaDatos<bool>> EliminarProducto(int id)
        {
            var respuesta = await _http.DeleteFromJsonAsync<RespuestaDatos<bool>>($"GestionProducto/Eliminar/{id}");
            return respuesta!;
        }

        public async Task<RespuestaDatos<List<ProductoDatos>>> ListarProductos(string busqueda)
        {
            var respuesta = await _http.GetFromJsonAsync<RespuestaDatos<List<ProductoDatos>>>($"GestionProducto/Listar/{busqueda}");
            return respuesta!;
        }

        public async Task<RespuestaDatos<ProductoDatos>> ObtenerProducto(int id)
        {
            var respuesta = await _http.GetFromJsonAsync<RespuestaDatos<ProductoDatos>>($"GestionProducto/Obtener/{id}");
            return respuesta!;
        }
    }
}
