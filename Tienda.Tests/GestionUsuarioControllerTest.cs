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
    public class GestionUsuarioControllerTests
    {
        private readonly Mock<IServicioUsuario> _mockServicioUsuario;
        private readonly GestionUsuarioController _controller;

        public GestionUsuarioControllerTests()
        {
            _mockServicioUsuario = new Mock<IServicioUsuario>();
            _controller = new GestionUsuarioController(_mockServicioUsuario.Object);
        }

        [Fact]
        public async Task ListarUsuarios_ReturnsOkResult_WithListOfUsuarios()
        {
            // Arrange
            var usuarios = new List<UsuarioDatos> { new UsuarioDatos { IdUsuario = 1, NombreCompleto = "Usuario 1" } };
            _mockServicioUsuario.Setup(serv => serv.ListarUsuarios(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(usuarios);

            // Act
            var result = await _controller.ListarUsuarios("Admin");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<List<UsuarioDatos>>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(usuarios, respuesta.Resultado);
        }

        [Fact]
        public async Task ObtenerUsuario_ReturnsOkResult_WithUsuario()
        {
            // Arrange
            var usuario = new UsuarioDatos { IdUsuario = 1, NombreCompleto = "Usuario 1" };
            _mockServicioUsuario.Setup(serv => serv.ObtenerUsuario(It.IsAny<int>())).ReturnsAsync(usuario);

            // Act
            var result = await _controller.ObtenerUsuario(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<UsuarioDatos>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(usuario, respuesta.Resultado);
        }

        [Fact]
        public async Task CrearUsuario_ReturnsOkResult_WithCreatedUsuario()
        {
            // Arrange
            var usuario = new UsuarioDatos { IdUsuario = 1, NombreCompleto = "Usuario 1" };
            _mockServicioUsuario.Setup(serv => serv.CrearUsuario(It.IsAny<UsuarioDatos>())).ReturnsAsync(usuario);

            // Act
            var result = await _controller.CrearUsuario(usuario);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<UsuarioDatos>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(usuario, respuesta.Resultado);
        }

        [Fact]
        public async Task ActualizarUsuario_ReturnsOkResult_WithBoolean()
        {
            // Arrange
            _mockServicioUsuario.Setup(serv => serv.ActualizarUsuario(It.IsAny<UsuarioDatos>())).ReturnsAsync(true);

            // Act
            var result = await _controller.ActualizarUsuario(new UsuarioDatos { IdUsuario = 1, NombreCompleto = "Usuario Actualizado" });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<bool>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.True(respuesta.Resultado);
        }

        [Fact]
        public async Task EliminarUsuario_ReturnsOkResult_WithBoolean()
        {
            // Arrange
            _mockServicioUsuario.Setup(serv => serv.EliminarUsuario(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.EliminarUsuario(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<bool>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.True(respuesta.Resultado);
        }

        [Fact]
        public async Task AutenticarUsuario_ReturnsOkResult_WithSesionDatos()
        {
            // Arrange
            var sesionDatos = new SesionDatos { IdUsuario = 1, NombreCompleto = "Usuario 1" };
            _mockServicioUsuario.Setup(serv => serv.AutenticarUsuario(It.IsAny<LoginDatos>())).ReturnsAsync(sesionDatos);

            // Act
            var result = await _controller.AutenticarUsuario(new LoginDatos { Correo = "usuario@example.com", Clave = "password" });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var respuesta = Assert.IsType<RespuestaDatos<SesionDatos>>(okResult.Value);
            Assert.True(respuesta.Ok);
            Assert.Equal(sesionDatos, respuesta.Resultado);
        }
    }
}
