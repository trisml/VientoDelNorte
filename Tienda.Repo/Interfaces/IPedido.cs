using System.Threading.Tasks;
using Tienda.Model;

namespace Tienda.Repo.Interfaces
{
    public interface IPedido : IEstandar<Pedido>
    {
        Task<Pedido> RegistrarPedido(Pedido pedido);
    }
}
