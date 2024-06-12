using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.Datos;

namespace Tienda.WebAssembly.Servicios.Interfaces
{
    public interface IServicioCarrito
    {
        event Action MostrarProductos;

        int ContarProductos();

        Task AgregarAlCarrito(CarritoDatos itemCarrito);

        Task EliminarDelCarrito(int idProducto);

        Task<List<CarritoDatos>> ObtenerCarrito();

        Task VaciarCarrito();
    }
}
