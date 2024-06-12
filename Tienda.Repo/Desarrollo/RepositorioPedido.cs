using System.Linq;
using System.Threading.Tasks;
using Tienda.Repo.Interfaces;
using Tienda.Repo.ApplicationDbContext;
using Tienda.Model;
using Microsoft.EntityFrameworkCore;

namespace Tienda.Repo.Desarrollo
{
    // Repositorio especifico para la entidad Pedido
    public class RepositorioPedido : EstandarRepositorio<Pedido>, IPedido
    {
        private readonly ProyectoTristanContext _contexto;

        
        public RepositorioPedido(ProyectoTristanContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        // Método para registrar un nuevo pedido
        public async Task<Pedido> RegistrarPedido(Pedido pedido)
        {
            // Inicia una transacción de base de datos 
            using var transaccion = await _contexto.Database.BeginTransactionAsync();

            try
            {
                // Obtiene los productos asociados al pedido
                var productos = await _contexto.Productos
                    .Where(p => pedido.DetallePedidos.Select(dp => dp.IdProducto).Contains(p.IdProducto))
                    .ToListAsync();

                // Actualiza la cantidad de cada producto según los detalles del pedido
                foreach (var producto in productos)
                {
                    var detallePedido = pedido.DetallePedidos.First(dp => dp.IdProducto == producto.IdProducto);
                    producto.Cantidad -= detallePedido.Cantidad;
                    _contexto.Productos.Update(producto);
                }

                // Añade el pedido a la base de datos
                await _contexto.Pedidos.AddAsync(pedido);
                await _contexto.SaveChangesAsync();

                // Confirma la transacción
                await transaccion.CommitAsync();

                return pedido;
            }
            catch
            {
                // Si ocurre un error, cancela la transacción
                await transaccion.RollbackAsync();
                throw;
            }
        }
    }
}
