using Microsoft.AspNetCore.Mvc;
using Tienda.Datos;
using Tienda.Servicio.Interfaces;

namespace Tienda.API.Controllers
{
    // Define el controlador con la ruta base "api/GestionCategoria"
    [Route("api/[controller]")]
    [ApiController]
    public class GestionCategoriaController : ControllerBase
    {
        private readonly IServicioCategoria _servicioCategoria;

        // Inyección de dependencias para el servicio de categoría
        public GestionCategoriaController(IServicioCategoria servicioCategoria)
        {
            _servicioCategoria = servicioCategoria;
        }

        // Endpoint para listar categorías con una búsqueda opcional
        [HttpGet("ListarCategorias/{busqueda?}")]
        public async Task<IActionResult> ListarCategorias(string busqueda = "NA")
        {
            var respuesta = new RespuestaDatos<List<CategoriaDatos>>();
            try
            {
                // Si no se proporciona un término de búsqueda, se usa una cadena vacía
                if (busqueda == "NA") busqueda = "";
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioCategoria.ListarCategorias(busqueda);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message; // Captura y devuelve el mensaje de error
            }
            return Ok(respuesta);
        }

        // Endpoint para obtener una categoría por su ID
        [HttpGet("ObtenerCategoria/{id:int}")]
        public async Task<IActionResult> ObtenerCategoria(int id)
        {
            var respuesta = new RespuestaDatos<CategoriaDatos>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioCategoria.ObtenerCategoria(id);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        // Endpoint para crear una nueva categoría
        [HttpPost("CrearCategoria")]
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaDatos categoria)
        {
            var respuesta = new RespuestaDatos<CategoriaDatos>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioCategoria.CrearCategoria(categoria);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        // Endpoint para actualizar una categoría existente
        [HttpPut("ActualizarCategoria")]
        public async Task<IActionResult> ActualizarCategoria([FromBody] CategoriaDatos categoria)
        {
            var respuesta = new RespuestaDatos<bool>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioCategoria.ActualizarCategoria(categoria);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        // Endpoint para eliminar una categoría por su ID
        [HttpDelete("EliminarCategoria/{id:int}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var respuesta = new RespuestaDatos<bool>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioCategoria.EliminarCategoria(id);
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
