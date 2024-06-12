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
    public class GestionProductoControllerTests
    {
        private readonly Mock<IServicioProducto> _mockServicioProducto;
        private readonly GestionProductoController _controller;

        public GestionProductoControllerTests()
        {
            _mockServicioProducto = new Mock<IServicioProducto>();
            _controller = new GestionProductoController(_mockServicioProducto.Object);
        }

        [Fact]
        public async Task ListarProductos_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var productos = new List<ProductoDatos> { new ProductoDatos { IdProducto = 1, Nombre = "Producto 1" } };
            _mockServicioProducto.Setup(serv => serv.ListarProductos(It.IsAny<string>())).ReturnsAsync(productos);

            // Act
            var result = await _controller.ListarProductos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<List<ProductoDatos>>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(productos, respuesta.Resultado);
        }

        [Fact]
        public async Task ObtenerProducto_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var producto = new ProductoDatos { IdProducto = 1, Nombre = "Producto 1" };
            _mockServicioProducto.Setup(serv => serv.ObtenerProducto(It.IsAny<int>())).ReturnsAsync(producto);

            // Act
            var result = await _controller.ObtenerProducto(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<ProductoDatos>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(producto, respuesta.Resultado);
        }

        [Fact]
        public async Task CrearProducto_ReturnsOkResult_WithCreatedProduct()
        {
            // Arrange
            var producto = new ProductoDatos { IdProducto = 1, Nombre = "Producto 1" };
            _mockServicioProducto.Setup(serv => serv.CrearProducto(It.IsAny<ProductoDatos>())).ReturnsAsync(producto);

            // Act
            var result = await _controller.CrearProducto(producto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<ProductoDatos>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(producto, respuesta.Resultado);
        }

        [Fact]
        public async Task ActualizarProducto_ReturnsOkResult_WithBoolean()
        {
            // Arrange
            _mockServicioProducto.Setup(serv => serv.ActualizarProducto(It.IsAny<ProductoDatos>())).ReturnsAsync(true);

            // Act
            var result = await _controller.ActualizarProducto(new ProductoDatos { IdProducto = 1, Nombre = "Producto Actualizado" });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<bool>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.True(respuesta.Resultado);
        }

        [Fact]
        public async Task EliminarProducto_ReturnsOkResult_WithBoolean()
        {
            // Arrange
            _mockServicioProducto.Setup(serv => serv.EliminarProducto(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.EliminarProducto(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<bool>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.True(respuesta.Resultado);
        }
    }
}
