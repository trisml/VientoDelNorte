using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Tienda.Datos;
using Tienda.WebAssembly.Servicios.Interfaces;

namespace Tienda.WebAssembly.Servicios.Desarrollo
{
    public class ServicioCarrito : IServicioCarrito
    {
        // Dependencias de servicios inyectados
        private readonly ILocalStorageService _almacenLocal;
        private readonly ISyncLocalStorageService _almacenLocalSync;
        private readonly IToastService _servicioToast;

        // Constructor para inyectar dependencias
        public ServicioCarrito(ILocalStorageService almacenLocal, ISyncLocalStorageService almacenLocalSync, IToastService servicioToast)
        {
            _almacenLocal = almacenLocal;
            _almacenLocalSync = almacenLocalSync;
            _servicioToast = servicioToast;
        }

        // Evento para notificar cambios en el carrito
        public event Action MostrarProductos;

        // Método para agregar un producto al carrito
        public async Task AgregarAlCarrito(CarritoDatos itemCarrito)
        {
            try
            {
                // Obtener el carrito desde el almacenamiento local, o inicializar uno nuevo si no existe
                var carrito = await _almacenLocal.GetItemAsync<List<CarritoDatos>>("carrito") ?? new List<CarritoDatos>();
                // Buscar si el producto ya está en el carrito
                var productoExistente = carrito.FirstOrDefault(c => c.Producto.IdProducto == itemCarrito.Producto.IdProducto);

                // Si el producto ya existe, eliminarlo para actualizarlo
                if (productoExistente != null)
                {
                    carrito.Remove(productoExistente);
                }

                // Agregar el nuevo producto o la actualización del producto existente
                carrito.Add(itemCarrito);
                // Guardar el carrito actualizado en el almacenamiento local
                await _almacenLocal.SetItemAsync("carrito", carrito);

                // Mostrar un mensaje de éxito dependiendo de si el producto fue añadido o actualizado
                if (productoExistente != null)
                {
                    _servicioToast.ShowSuccess("Producto actualizado en el carrito");
                }
                else
                {
                    _servicioToast.ShowSuccess("Producto añadido al carrito");
                }

                // Invocar el evento para notificar cambios en el carrito
                MostrarProductos.Invoke();
            }
            catch
            {
                // Mostrar un mensaje de error si ocurre una excepción
                _servicioToast.ShowError("Error al agregar el producto al carrito");
            }
        }

        // Método para contar la cantidad de productos en el carrito
        public int ContarProductos()
        {
            // Obtener el carrito desde el almacenamiento local de forma síncrona
            var carrito = _almacenLocalSync.GetItem<List<CarritoDatos>>("carrito");
            // Retornar la cantidad de productos en el carrito, o 0 si el carrito es null
            return carrito?.Count ?? 0;
        }

        // Método para eliminar un producto del carrito por su ID
        public async Task EliminarDelCarrito(int idProducto)
        {
            // Obtener el carrito desde el almacenamiento local
            var carrito = await _almacenLocal.GetItemAsync<List<CarritoDatos>>("carrito");

            if (carrito != null)
            {
                // Buscar el producto en el carrito
                var producto = carrito.FirstOrDefault(c => c.Producto.IdProducto == idProducto);

                if (producto != null)
                {
                    // Eliminar el producto del carrito y guardar los cambios
                    carrito.Remove(producto);
                    await _almacenLocal.SetItemAsync("carrito", carrito);
                    // Invocar el evento para notificar cambios en el carrito
                    MostrarProductos.Invoke();
                }
            }
        }

        // Método para obtener la lista de productos en el carrito
        public async Task<List<CarritoDatos>> ObtenerCarrito()
        {
            // Obtener el carrito desde el almacenamiento local, o inicializar una lista vacía si no existe
            var carrito = await _almacenLocal.GetItemAsync<List<CarritoDatos>>("carrito");
            return carrito ?? new List<CarritoDatos>();
        }

        // Método para vaciar el carrito
        public async Task VaciarCarrito()
        {
            // Remover el carrito del almacenamiento local
            await _almacenLocal.RemoveItemAsync("carrito");
            // Invocar el evento para notificar cambios en el carrito
            MostrarProductos.Invoke();
        }
    }
}
