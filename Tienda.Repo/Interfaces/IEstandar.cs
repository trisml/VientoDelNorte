using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tienda.Repo.Interfaces
{
    public interface IEstandar<T> where T : class
    {
        

        Task<T> Crear(T entidad);

        Task<bool> Editar(T entidad);

        IQueryable<T> Listar(Expression<Func<T, bool>>? filtro = null);
        Task<bool> Eliminar(T entidad);
    }
}
