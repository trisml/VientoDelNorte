using System.Threading.Tasks;
using System.Collections.Generic;
using Tienda.Datos;

namespace Tienda.WebAssembly.Servicios.Interfaces
{
    public interface IServicioProducto
    {
        Task<RespuestaDatos<ProductoDatos>> CrearProducto(ProductoDatos producto);

        Task<RespuestaDatos<List<ProductoDatos>>> ListarProductos(string busqueda);

        Task<RespuestaDatos<ProductoDatos>> ObtenerProducto(int id);

        Task<RespuestaDatos<List<ProductoDatos>>> ObtenerCatalogo(string categoria, string busqueda);

        Task<RespuestaDatos<bool>> ActualizarProducto(ProductoDatos producto);

        Task<RespuestaDatos<bool>> EliminarProducto(int id);
    }
}
