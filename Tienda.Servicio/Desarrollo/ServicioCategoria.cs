using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tienda.Datos;
using Tienda.Model;
using Tienda.Repo.Interfaces;
using Tienda.Servicio.Interfaces;

namespace Tienda.Servicio.Desarrollo
{
    public class ServicioCategoria : IServicioCategoria
    {
        private readonly IEstandar<Categoria> _repositorioCategoria;
        private readonly IMapper _mapeador;

        // Inyecta las dependencias necesarias
        public ServicioCategoria(IEstandar<Categoria> repositorioCategoria, IMapper mapeador)
        {
            _repositorioCategoria = repositorioCategoria;
            _mapeador = mapeador;
        }

        // Método para crear una nueva categoría
        public async Task<CategoriaDatos> CrearCategoria(CategoriaDatos categoria)
        {
            try
            {
                // Mapea los datos de la categoría a un modelo de base de datos
                var modeloBd = _mapeador.Map<Categoria>(categoria);
                // Crea la categoría en la base de datos
                var resultado = await _repositorioCategoria.Crear(modeloBd);

                if (resultado.IdCategoria != 0)
                {
                    // Si se crea correctamente, mapea de vuelta al DTO y lo devuelve
                    return _mapeador.Map<CategoriaDatos>(resultado);
                }
                else
                {
                    throw new TaskCanceledException("No se pudo crear la categoría");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear la categoría", ex);
            }
        }

        // Método para actualizar una categoría existente
        public async Task<bool> ActualizarCategoria(CategoriaDatos categoria)
        {
            try
            {
                // Busca la categoría en la base de datos
                var consulta = _repositorioCategoria.Listar(c => c.IdCategoria == categoria.IdCategoria);
                var modeloBd = await consulta.FirstOrDefaultAsync();
                if (modeloBd != null)
                {
                    // Actualiza el nombre de la categoría
                    modeloBd.Nombre = categoria.Nombre;
                    // Guarda los cambios en la base de datos
                    var resultado = await _repositorioCategoria.Editar(modeloBd);
                    if (!resultado)
                    {
                        throw new TaskCanceledException("No se pudo actualizar la categoría");
                    }
                    return resultado;
                }
                else
                {
                    throw new TaskCanceledException("Categoría no encontrada");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar la categoría", ex);
            }
        }

        // Método para eliminar una categoría existente
        public async Task<bool> EliminarCategoria(int id)
        {
            try
            {
                // Busca la categoría en la base de datos
                var consulta = _repositorioCategoria.Listar(c => c.IdCategoria == id);
                var modeloBd = await consulta.FirstOrDefaultAsync();

                if (modeloBd != null)
                {
                    // Elimina la categoría de la base de datos
                    var resultado = await _repositorioCategoria.Eliminar(modeloBd);
                    if (!resultado)
                    {
                        throw new TaskCanceledException("No se pudo eliminar la categoría");
                    }
                    return resultado;
                }
                else
                {
                    throw new TaskCanceledException("Categoría no encontrada");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar la categoría", ex);
            }
        }

        // Método para listar las categorias según un parametro
        public async Task<List<CategoriaDatos>> ListarCategorias(string busqueda)
        {
            try
            {
                // Filtra las categorías que contienen el parametro
                var consulta = _repositorioCategoria.Listar(c => c.Nombre!.ToLower().Contains(busqueda.ToLower()));
                var listaCategorias = _mapeador.Map<List<CategoriaDatos>>(await consulta.ToListAsync());
                return listaCategorias;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al listar las categorías", ex);
            }
        }

        // Método para obtener una categoría por su ID
        public async Task<CategoriaDatos> ObtenerCategoria(int id)
        {
            try
            {
                // Busca la categoría en la base de datos por su ID
                var consulta = _repositorioCategoria.Listar(c => c.IdCategoria == id);
                var modeloBd = await consulta.FirstOrDefaultAsync();

                if (modeloBd != null)
                {
                    // Si se encuentra, mapea al DTO y lo devuelve
                    return _mapeador.Map<CategoriaDatos>(modeloBd);
                }
                else
                {
                    throw new TaskCanceledException("Categoría no encontrada");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener la categoría", ex);
            }
        }
    }
}
