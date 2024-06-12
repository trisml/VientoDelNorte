using Microsoft.AspNetCore.Mvc;
using Tienda.Datos;
using Tienda.Servicio.Interfaces;

namespace Tienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionProductoController : ControllerBase
    {
        private readonly IServicioProducto _servicioProducto;

        public GestionProductoController(IServicioProducto servicioProducto)
        {
            _servicioProducto = servicioProducto;
        }

        [HttpGet("Listar/{buscar?}")]
        public async Task<IActionResult> ListarProductos(string buscar = "NA")
        {
            var respuesta = new RespuestaDatos<List<ProductoDatos>>();
            try
            {
                if (buscar == "NA") buscar = "";
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioProducto.ListarProductos(buscar);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpGet("Catalogo/{categoria?}/{buscar?}")]
        public async Task<IActionResult> ObtenerCatalogo(string categoria, string buscar = "NA")
        {
            var respuesta = new RespuestaDatos<List<ProductoDatos>>();
            try
            {
                if (categoria.ToLower() == "todos") categoria = "";
                if (buscar == "NA") buscar = "";
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioProducto.ObtenerCatalogo(categoria, buscar);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpGet("Obtener/{id:int}")]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            var respuesta = new RespuestaDatos<ProductoDatos>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioProducto.ObtenerProducto(id);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoDatos producto)
        {
            var respuesta = new RespuestaDatos<ProductoDatos>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioProducto.CrearProducto(producto);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarProducto([FromBody] ProductoDatos producto)
        {
            var respuesta = new RespuestaDatos<bool>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioProducto.ActualizarProducto(producto);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var respuesta = new RespuestaDatos<bool>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioProducto.EliminarProducto(id);
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
