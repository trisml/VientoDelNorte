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
    public class GestionCategoriaControllerTests
    {
        private readonly Mock<IServicioCategoria> _mockServicioCategoria;
        private readonly GestionCategoriaController _controller;

        public GestionCategoriaControllerTests()
        {
            _mockServicioCategoria = new Mock<IServicioCategoria>();
            _controller = new GestionCategoriaController(_mockServicioCategoria.Object);
        }

        [Fact]
        public async Task ListarCategorias_ReturnsOkResult_WithListOfCategorias()
        {
            // Arrange
            var categorias = new List<CategoriaDatos> { new CategoriaDatos { IdCategoria = 1, Nombre = "Categoria 1" } };
            _mockServicioCategoria.Setup(serv => serv.ListarCategorias(It.IsAny<string>())).ReturnsAsync(categorias);

            // Act
            var result = await _controller.ListarCategorias();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<List<CategoriaDatos>>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(categorias, respuesta.Resultado);
        }

        [Fact]
        public async Task ObtenerCategoria_ReturnsOkResult_WithCategoria()
        {
            // Arrange
            var categoria = new CategoriaDatos { IdCategoria = 1, Nombre = "Categoria 1" };
            _mockServicioCategoria.Setup(serv => serv.ObtenerCategoria(It.IsAny<int>())).ReturnsAsync(categoria);

            // Act
            var result = await _controller.ObtenerCategoria(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<CategoriaDatos>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(categoria, respuesta.Resultado);
        }

        [Fact]
        public async Task CrearCategoria_ReturnsOkResult_WithCreatedCategoria()
        {
            // Arrange
            var categoria = new CategoriaDatos { IdCategoria = 1, Nombre = "Categoria 1" };
            _mockServicioCategoria.Setup(serv => serv.CrearCategoria(It.IsAny<CategoriaDatos>())).ReturnsAsync(categoria);

            // Act
            var result = await _controller.CrearCategoria(categoria);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<CategoriaDatos>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(categoria, respuesta.Resultado);
        }

        [Fact]
        public async Task ActualizarCategoria_ReturnsOkResult_WithBoolean()
        {
            // Arrange
            _mockServicioCategoria.Setup(serv => serv.ActualizarCategoria(It.IsAny<CategoriaDatos>())).ReturnsAsync(true);

            // Act
            var result = await _controller.ActualizarCategoria(new CategoriaDatos { IdCategoria = 1, Nombre = "Categoria Actualizada" });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<bool>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.True(respuesta.Resultado);
        }

        [Fact]
        public async Task EliminarCategoria_ReturnsOkResult_WithBoolean()
        {
            // Arrange
            _mockServicioCategoria.Setup(serv => serv.EliminarCategoria(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.EliminarCategoria(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<bool>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.True(respuesta.Resultado);
        }
    }
}
