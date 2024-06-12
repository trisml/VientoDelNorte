using System.Threading.Tasks;
using System.Collections.Generic;
using Tienda.Datos;

namespace Tienda.WebAssembly.Servicios.Interfaces
{
    public interface IServicioCategoria
    {
        Task<RespuestaDatos<CategoriaDatos>> CrearCategoria(CategoriaDatos categoria);

        Task<RespuestaDatos<List<CategoriaDatos>>> ListarCategorias(string busqueda);

        Task<RespuestaDatos<CategoriaDatos>> ObtenerCategoria(int id);

        Task<RespuestaDatos<bool>> ActualizarCategoria(CategoriaDatos categoria);

        Task<RespuestaDatos<bool>> EliminarCategoria(int id);
    }
}
