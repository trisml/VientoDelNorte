using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.API.Controllers;
using Tienda.Datos;
using Tienda.Servicio.Interfaces;
using Xunit;

namespace Tienda.Tests
{
    public class GestionPedidoControllerTests
    {
        private readonly Mock<IServicioPedido> _mockServicioPedido;
        private readonly GestionPedidoController _controller;

        public GestionPedidoControllerTests()
        {
            _mockServicioPedido = new Mock<IServicioPedido>();
            _controller = new GestionPedidoController(_mockServicioPedido.Object);
        }

        [Fact]
        public async Task RegistrarPedido_ReturnsOkResult_WithCreatedPedido()
        {
            // Arrange
            var pedido = new PedidoDatos { IdPedido = 1, IdUsuario = 1 };
            _mockServicioPedido.Setup(serv => serv.RegistrarPedido(It.IsAny<PedidoDatos>())).ReturnsAsync(pedido);

            // Act
            var result = await _controller.RegistrarPedido(pedido);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<PedidoDatos>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(pedido, respuesta.Resultado);
        }

        [Fact]
        public async Task ListarPedidosPorUsuario_ReturnsOkResult_WithListOfPedidos()
        {
            // Arrange
            var pedidos = new List<PedidoDatos> { new PedidoDatos { IdPedido = 1, IdUsuario = 1 } };
            _mockServicioPedido.Setup(serv => serv.ListarPedidosPorUsuario(It.IsAny<int>())).ReturnsAsync(pedidos);

            // Act
            var result = await _controller.ListarPedidosPorUsuario(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<List<PedidoDatos>>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(pedidos, respuesta.Resultado);
        }

        [Fact]
        public async Task ObtenerPedido_ReturnsOkResult_WithPedido()
        {
            // Arrange
            var pedido = new PedidoDatos { IdPedido = 1, IdUsuario = 1 };
            _mockServicioPedido.Setup(serv => serv.ObtenerPedido(It.IsAny<int>())).ReturnsAsync(pedido);

            // Act
            var result = await _controller.ObtenerPedido(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<PedidoDatos>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(pedido, respuesta.Resultado);
        }

        [Fact]
        public async Task EliminarPedido_ReturnsOkResult_WithBoolean()
        {
            // Arrange
            _mockServicioPedido.Setup(serv => serv.EliminarPedido(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.EliminarPedido(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<bool>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.True(respuesta.Resultado);
        }
    }
}
