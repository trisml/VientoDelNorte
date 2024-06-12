using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tienda.Datos;
using Tienda.Model;
using Tienda.Repo.Interfaces;
using Tienda.Servicio.Interfaces;

namespace Tienda.Servicio.Desarrollo
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly IEstandar<Usuario> _repositorioUsuario;
        private readonly IMapper _mapeador;

        public ServicioUsuario(IEstandar<Usuario> repositorioUsuario, IMapper mapeador)
        {
            _repositorioUsuario = repositorioUsuario;
            _mapeador = mapeador;
        }

        // Método para autenticar al usuario
        public async Task<SesionDatos> AutenticarUsuario(LoginDatos login)
        {
            try
            {
                // Busca al usuario por correo y clave
                var consulta = _repositorioUsuario.Listar(u => u.Correo == login.Correo && u.Clave == login.Clave);
                var modeloBd = await consulta.FirstOrDefaultAsync();

                if (modeloBd != null)
                {
                    return _mapeador.Map<SesionDatos>(modeloBd);
                }
                else
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al autenticar el usuario", ex);
            }
        }

        // Método para crear un nuevo usuario
        public async Task<UsuarioDatos> CrearUsuario(UsuarioDatos usuario)
        {
            try
            {
                var modeloBd = _mapeador.Map<Usuario>(usuario);
                var resultado = await _repositorioUsuario.Crear(modeloBd);

                if (resultado.IdUsuario != 0)
                {
                    return _mapeador.Map<UsuarioDatos>(resultado);
                }
                else
                {
                    throw new TaskCanceledException("No se pudo crear el usuario");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear el usuario", ex);
            }
        }

        // Método para actualizar un usuario existente
        public async Task<bool> ActualizarUsuario(UsuarioDatos usuario)
        {
            try
            {
                var consulta = _repositorioUsuario.Listar(u => u.IdUsuario == usuario.IdUsuario);
                var modeloBd = await consulta.FirstOrDefaultAsync();
                if (modeloBd != null)
                {
                    // Actualiza los campos del usuario
                    modeloBd.NombreCompleto = usuario.NombreCompleto;
                    modeloBd.Correo = usuario.Correo;
                    modeloBd.Clave = usuario.Clave;

                    var resultado = await _repositorioUsuario.Editar(modeloBd);
                    if (!resultado)
                    {
                        throw new TaskCanceledException("No se pudo actualizar el usuario");
                    }
                    return resultado;
                }
                else
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar el usuario", ex);
            }
        }

        // Método para eliminar un usuario por su ID
        public async Task<bool> EliminarUsuario(int id)
        {
            try
            {
                var consulta = _repositorioUsuario.Listar(u => u.IdUsuario == id);
                var modeloBd = await consulta.FirstOrDefaultAsync();

                if (modeloBd != null)
                {
                    var resultado = await _repositorioUsuario.Eliminar(modeloBd);
                    if (!resultado)
                    {
                        throw new TaskCanceledException("No se pudo eliminar el usuario");
                    }
                    return resultado;
                }
                else
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar el usuario", ex);
            }
        }

        // Método para listar los usuarios filtrados por rol y búsqueda
        public async Task<List<UsuarioDatos>> ListarUsuarios(string rol, string busqueda)
        {
            try
            {
                var consulta = _repositorioUsuario.Listar(u =>
                    u.Rol == rol && string.Concat(u.NombreCompleto.ToLower(), u.Correo.ToLower()).Contains(busqueda.ToLower()));

                var listaUsuarios = _mapeador.Map<List<UsuarioDatos>>(await consulta.ToListAsync());
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al listar los usuarios", ex);
            }
        }

        // Método para obtener un usuario por su ID
        public async Task<UsuarioDatos> ObtenerUsuario(int id)
        {
            try
            {
                var consulta = _repositorioUsuario.Listar(u => u.IdUsuario == id);
                var modeloBd = await consulta.FirstOrDefaultAsync();

                if (modeloBd != null)
                {
                    return _mapeador.Map<UsuarioDatos>(modeloBd);
                }
                else
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener el usuario", ex);
            }
        }
    }
}
