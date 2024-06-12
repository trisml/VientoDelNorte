using Microsoft.AspNetCore.Mvc;
using Tienda.Datos;
using Tienda.Servicio.Interfaces;

namespace Tienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionPedidoController : ControllerBase
    {
        private readonly IServicioPedido _servicioPedido;

        public GestionPedidoController(IServicioPedido servicioPedido)
        {
            _servicioPedido = servicioPedido;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarPedido([FromBody] PedidoDatos pedido)
        {
            var respuesta = new RespuestaDatos<PedidoDatos>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioPedido.RegistrarPedido(pedido);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpGet("ListarPorUsuario/{idUsuario}")]
        public async Task<IActionResult> ListarPedidosPorUsuario(int idUsuario)
        {
            var respuesta = new RespuestaDatos<List<PedidoDatos>>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioPedido.ListarPedidosPorUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> ObtenerPedido(int id)
        {
            var respuesta = new RespuestaDatos<PedidoDatos>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioPedido.ObtenerPedido(id);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            var respuesta = new RespuestaDatos<bool>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioPedido.EliminarPedido(id);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }
    }
}
