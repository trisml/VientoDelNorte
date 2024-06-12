using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.Datos;

namespace Tienda.Servicio.Interfaces
{
    public interface IServicioCategoria
    {
        Task<List<CategoriaDatos>> ListarCategorias(string busqueda);

        Task<CategoriaDatos> ObtenerCategoria(int id);

        Task<CategoriaDatos> CrearCategoria(CategoriaDatos categoria);

        Task<bool> ActualizarCategoria(CategoriaDatos categoria);

        Task<bool> EliminarCategoria(int id);
    }
}
