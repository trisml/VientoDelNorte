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
    public class ServicioPedido : IServicioPedido
    {
        private readonly IPedido _repositorioPedido;
        private readonly IMapper _mapeador;

        public ServicioPedido(IPedido repositorioPedido, IMapper mapeador)
        {
            _repositorioPedido = repositorioPedido;
            _mapeador = mapeador;
        }

        // Método para listar los pedidos de un usuario específico
        public async Task<List<PedidoDatos>> ListarPedidosPorUsuario(int idUsuario)
        {
            try
            {
                // Consulta los pedidos del usuario y carga los detalles de los productos
                var consulta = _repositorioPedido.Listar(p => p.IdUsuario == idUsuario);
                consulta = consulta.Include(p => p.DetallePedidos)
                                   .ThenInclude(dp => dp.IdProductoNavigation);

                var listaPedidos = _mapeador.Map<List<PedidoDatos>>(await consulta.ToListAsync());
                return listaPedidos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al listar los pedidos del usuario", ex);
            }
        }

        // Método para obtener un pedido por su ID
        public async Task<PedidoDatos> ObtenerPedido(int id)
        {
            try
            {
                // Consulta el pedido por su ID y carga los detalles de los productos
                var consulta = _repositorioPedido.Listar(p => p.IdPedido == id);
                consulta = consulta.Include(p => p.DetallePedidos)
                                   .ThenInclude(dp => dp.IdProductoNavigation);

                var modeloBd = await consulta.FirstOrDefaultAsync();

                if (modeloBd != null)
                {
                    return _mapeador.Map<PedidoDatos>(modeloBd);
                }
                else
                {
                    throw new TaskCanceledException("Pedido no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener el pedido", ex);
            }
        }

        // Método para eliminar un pedido por su ID
        public async Task<bool> EliminarPedido(int id)
        {
            try
            {
                // Consulta el pedido por su ID
                var consulta = _repositorioPedido.Listar(p => p.IdPedido == id);
                var modeloBd = await consulta.FirstOrDefaultAsync();

                if (modeloBd != null)
                {
                    // Elimina el pedido de la base de datos
                    var resultado = await _repositorioPedido.Eliminar(modeloBd);
                    if (!resultado)
                    {
                        throw new TaskCanceledException("No se pudo eliminar el pedido");
                    }
                    return resultado;
                }
                else
                {
                    throw new TaskCanceledException("Pedido no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar el pedido", ex);
            }
        }

        // Método para registrar un nuevo pedido
        public async Task<PedidoDatos> RegistrarPedido(PedidoDatos pedido)
        {
            try
            {
                // Mapea los datos del pedido a un modelo de base de datos
                var modeloBd = _mapeador.Map<Pedido>(pedido);
                // Registra el pedido en la base de datos
                var resultado = await _repositorioPedido.RegistrarPedido(modeloBd);

                if (resultado.IdPedido != 0)
                {
                    return _mapeador.Map<PedidoDatos>(resultado);
                }
                else
                {
                    throw new TaskCanceledException("No se pudo registrar el pedido");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al registrar el pedido", ex);
            }
        }
    }
}
