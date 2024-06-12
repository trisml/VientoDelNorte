using System.Threading.Tasks;
using System.Collections.Generic;
using Tienda.Datos;

namespace Tienda.WebAssembly.Servicios.Interfaces
{
    public interface IServicioPedido
    {
        Task<RespuestaDatos<PedidoDatos>> RegistrarPedido(PedidoDatos pedido);

        Task<RespuestaDatos<List<PedidoDatos>>> ListarPedidosPorUsuario(int idUsuario);

        Task<RespuestaDatos<PedidoDatos>> ObtenerPedido(int id);

        Task<RespuestaDatos<bool>> EliminarPedido(int id);
    }
}
