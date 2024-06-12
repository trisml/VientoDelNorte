using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tienda.Datos;
using Tienda.Model;
using Tienda.Repo.Interfaces;
using Tienda.Servicio.Interfaces;

namespace Tienda.Servicio.Desarrollo
{
    public class ServicioProducto : IServicioProducto
    {
        private readonly IEstandar<Producto> _repositorioProducto;
        private readonly IMapper _mapeador;

        public ServicioProducto(IEstandar<Producto> repositorioProducto, IMapper mapeador)
        {
            _repositorioProducto = repositorioProducto;
            _mapeador = mapeador;
        }

        // Método para obtener el catálogo de productos filtrado por categoría y búsqueda
        public async Task<List<ProductoDatos>> ObtenerCatalogo(string categoria, string busqueda)
        {
            try
            {
                // Consulta los productos que coinciden con los filtros de búsqueda y categoría
                var consulta = _repositorioProducto.Listar(p =>
                    p.Nombre.ToLower().Contains(busqueda.ToLower()) &&
                    p.IdCategoriaNavigation.Nombre.ToLower().Contains(categoria.ToLower()));

                var listaProductos = _mapeador.Map<List<ProductoDatos>>(await consulta.ToListAsync());
                return listaProductos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener el catálogo de productos", ex);
            }
        }

        // Método para crear un nuevo producto
        public async Task<ProductoDatos> CrearProducto(ProductoDatos producto)
        {
            try
            {
                var modeloBd = _mapeador.Map<Producto>(producto);
                var resultado = await _repositorioProducto.Crear(modeloBd);

                if (resultado.IdProducto != 0)
                {
                    return _mapeador.Map<ProductoDatos>(resultado);
                }
                else
                {
                    throw new TaskCanceledException("No se pudo crear el producto");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear el producto", ex);
            }
        }

        // Método para actualizar un producto existente
        public async Task<bool> ActualizarProducto(ProductoDatos producto)
        {
            try
            {
                var consulta = _repositorioProducto.Listar(p => p.IdProducto == producto.IdProducto);
                var modeloBd = await consulta.FirstOrDefaultAsync();
                if (modeloBd != null)
                {
                    // Actualiza los campos del producto
                    modeloBd.Nombre = producto.Nombre;
                    modeloBd.Descripcion = producto.Descripcion;
                    modeloBd.IdCategoria = producto.IdCategoria;
                    modeloBd.Precio = producto.Precio;
                    modeloBd.PrecioOferta = producto.PrecioOferta;
                    modeloBd.Cantidad = producto.Cantidad;
                    modeloBd.Imagen = producto.Imagen;

                    var resultado = await _repositorioProducto.Editar(modeloBd);
                    if (!resultado)
                    {
                        throw new TaskCanceledException("No se pudo actualizar el producto");
                    }
                    return resultado;
                }
                else
                {
                    throw new TaskCanceledException("Producto no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar el producto", ex);
            }
        }

        // Método para eliminar un producto por su ID
        public async Task<bool> EliminarProducto(int id)
        {
            try
            {
                var consulta = _repositorioProducto.Listar(p => p.IdProducto == id);
                var modeloBd = await consulta.FirstOrDefaultAsync();

                if (modeloBd != null)
                {
                    var resultado = await _repositorioProducto.Eliminar(modeloBd);
                    if (!resultado)
                    {
                        throw new TaskCanceledException("No se pudo eliminar el producto");
                    }
                    return resultado;
                }
                else
                {
                    throw new TaskCanceledException("Producto no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar el producto", ex);
            }
        }

        // Método para listar los productos segun parametro
        public async Task<List<ProductoDatos>> ListarProductos(string busqueda)
        {
            try
            {
                // Consulta los productos que coinciden con el paraemtro e incluye la información de la categoría
                var consulta = _repositorioProducto.Listar(p => p.Nombre.ToLower().Contains(busqueda.ToLower()));
                consulta = consulta.Include(p => p.IdCategoriaNavigation);

                var listaProductos = _mapeador.Map<List<ProductoDatos>>(await consulta.ToListAsync());
                return listaProductos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al listar los productos", ex);
            }
        }

        // Método para obtener un producto por su ID
        public async Task<ProductoDatos> ObtenerProducto(int id)
        {
            try
            {
                var consulta = _repositorioProducto.Listar(p => p.IdProducto == id);
                consulta = consulta.Include(p => p.IdCategoriaNavigation);
                var modeloBd = await consulta.FirstOrDefaultAsync();

                if (modeloBd != null)
                {
                    return _mapeador.Map<ProductoDatos>(modeloBd);
                }
                else
                {
                    throw new TaskCanceledException("Producto no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener el producto", ex);
            }
        }
    }
}
