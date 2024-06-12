using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tienda.Repo.Interfaces;
using Tienda.Repo.ApplicationDbContext;

namespace Tienda.Repo.Desarrollo
{
    // Repositorio estándar que implementa las operaciones CRUD básicas para cualquier entidad
    public class EstandarRepositorio<T> : IEstandar<T> where T : class
    {
        private readonly ProyectoTristanContext _contexto;

        // Constructor que inicializa el contexto de la base de datos
        public EstandarRepositorio(ProyectoTristanContext contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        // Método para crear una nueva entidad en la base de datos
        public async Task<T> Crear(T entidad)
        {
            if (entidad == null) throw new ArgumentNullException(nameof(entidad));

            try
            {
                await _contexto.Set<T>().AddAsync(entidad);
                await _contexto.SaveChangesAsync();
                return entidad;
            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException("Error al crear la entidad", dbEx);
            }
        }

        // Método para actualizar una entidad existente en la base de datos
        public async Task<bool> Editar(T entidad)
        {
            if (entidad == null) throw new ArgumentNullException(nameof(entidad));

            try
            {
                _contexto.Set<T>().Update(entidad);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                throw new InvalidOperationException("Error al actualizar la entidad", dbEx);
            }
        }

        // Método para eliminar una entidad de la base de datos
        public async Task<bool> Eliminar(T entidad)
        {
            if (entidad == null) throw new ArgumentNullException(nameof(entidad));

            try
            {
                _contexto.Set<T>().Remove(entidad);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException("Error al eliminar la entidad", dbEx);
            }
        }

        // Método para listar las entidades, con un filtro opcional
        public IQueryable<T> Listar(Expression<Func<T, bool>>? filtro = null)
        {
            // Si no se proporciona un filtro, se devuelven todas las entidades
            return filtro == null ? _contexto.Set<T>() : _contexto.Set<T>().Where(filtro);
        }
    }
}
