using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.Datos;

namespace Tienda.Servicio.Interfaces
{
    public interface IServicioProducto
    {
        Task<List<ProductoDatos>> ListarProductos(string busqueda);

        Task<List<ProductoDatos>> ObtenerCatalogo(string categoria, string busqueda);

        Task<ProductoDatos> ObtenerProducto(int id);

        Task<ProductoDatos> CrearProducto(ProductoDatos producto);

        Task<bool> ActualizarProducto(ProductoDatos producto);

        Task<bool> EliminarProducto(int id);
    }
}
