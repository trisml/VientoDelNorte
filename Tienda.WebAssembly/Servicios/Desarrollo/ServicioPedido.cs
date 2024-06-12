using Tienda.Datos;
using Tienda.WebAssembly.Servicios.Interfaces;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tienda.WebAssembly.Servicios.Desarrollo
{
    public class ServicioPedido : IServicioPedido
    {
        private readonly HttpClient _http;

        public ServicioPedido(HttpClient http)
        {
            _http = http;
        }

        public async Task<RespuestaDatos<PedidoDatos>> RegistrarPedido(PedidoDatos pedido)
        {
            var respuesta = await _http.PostAsJsonAsync("GestionPedido/Registrar", pedido);
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaDatos<PedidoDatos>>();
            return resultado!;
        }

        public async Task<RespuestaDatos<List<PedidoDatos>>> ListarPedidosPorUsuario(int idUsuario)
        {
            var respuesta = await _http.GetFromJsonAsync<RespuestaDatos<List<PedidoDatos>>>($"GestionPedido/ListarPorUsuario/{idUsuario}");
            return respuesta!;
        }

        public async Task<RespuestaDatos<PedidoDatos>> ObtenerPedido(int id)
        {
            var respuesta = await _http.GetFromJsonAsync<RespuestaDatos<PedidoDatos>>($"GestionPedido/Obtener/{id}");
            return respuesta!;
        }

        public async Task<RespuestaDatos<bool>> EliminarPedido(int id)
        {
            var respuesta = await _http.DeleteAsync($"GestionPedido/Eliminar/{id}");
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaDatos<bool>>();
            return resultado!;
        }
    }
}
