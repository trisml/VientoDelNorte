using AutoMapper;
using Tienda.Model;
using Tienda.Datos;

namespace Tienda.Mapeado
{
    // Define un perfil de AutoMapper para configurar los mapeos entre los modelos y los DTOs
    public class PerfilAutoMapper : Profile
    {
        public PerfilAutoMapper()
        {
            // Mapeo entre la entidad Usuario y el DTO UsuarioDatos
            CreateMap<Usuario, UsuarioDatos>();
            CreateMap<Usuario, SesionDatos>(); // Mapeo específico para datos de sesión
            CreateMap<UsuarioDatos, Usuario>();

            // Mapeo entre la entidad Categoria y el DTO CategoriaDatos
            CreateMap<Categoria, CategoriaDatos>();
            CreateMap<CategoriaDatos, Categoria>();

            // Mapeo entre la entidad Producto y el DTO ProductoDatos
            CreateMap<Producto, ProductoDatos>();

            // Ignora la navegación de IdCategoria al mapear de ProductoDatos a Producto
            CreateMap<ProductoDatos, Producto>().ForMember(d => d.IdCategoriaNavigation, opc => opc.Ignore());

            // Mapeo entre la entidad DetallePedido y el DTO DetallePedidoDatos
            CreateMap<DetallePedido, DetallePedidoDatos>();
            CreateMap<DetallePedidoDatos, DetallePedido>();

            // Mapeo entre la entidad Pedido y el DTO PedidoDatos
            CreateMap<Pedido, PedidoDatos>();
            CreateMap<PedidoDatos, Pedido>();
        }
    }
}
