using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.Datos;

namespace Tienda.Servicio.Interfaces
{
    public interface IServicioPedido
    {
        Task<PedidoDatos> RegistrarPedido(PedidoDatos pedido);

        Task<List<PedidoDatos>> ListarPedidosPorUsuario(int idUsuario);

        Task<PedidoDatos> ObtenerPedido(int id);

        Task<bool> EliminarPedido(int id);
    }
}
