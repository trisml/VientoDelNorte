using Microsoft.AspNetCore.Mvc;
using Tienda.Datos;
using Tienda.Servicio.Interfaces;

namespace Tienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionUsuarioController : ControllerBase
    {
        private readonly IServicioUsuario _servicioUsuario;

        public GestionUsuarioController(IServicioUsuario servicioUsuario)
        {
            _servicioUsuario = servicioUsuario;
        }

        [HttpGet("Listar/{rol:alpha}/{buscar:alpha?}")]
        public async Task<IActionResult> ListarUsuarios(string rol, string buscar = "NA")
        {
            var respuesta = new RespuestaDatos<List<UsuarioDatos>>();
            try
            {
                if (buscar == "NA") buscar = "";
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioUsuario.ListarUsuarios(rol, buscar);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpGet("Obtener/{id:int}")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var respuesta = new RespuestaDatos<UsuarioDatos>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioUsuario.ObtenerUsuario(id);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDatos usuario)
        {
            var respuesta = new RespuestaDatos<UsuarioDatos>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioUsuario.CrearUsuario(usuario);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario([FromBody] LoginDatos login)
        {
            var respuesta = new RespuestaDatos<SesionDatos>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioUsuario.AutenticarUsuario(login);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] UsuarioDatos usuario)
        {
            var respuesta = new RespuestaDatos<bool>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioUsuario.ActualizarUsuario(usuario);
            }
            catch (Exception ex)
            {
                respuesta.Ok = false;
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var respuesta = new RespuestaDatos<bool>();
            try
            {
                respuesta.Ok = true;
                respuesta.Resultado = await _servicioUsuario.EliminarUsuario(id);
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
