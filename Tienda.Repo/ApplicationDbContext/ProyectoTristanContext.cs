using Microsoft.EntityFrameworkCore;
using Tienda.Model;

namespace Tienda.Repo.ApplicationDbContext
{
    // Contexto de base de datos de Entity Framework para el proyecto "ProyectoTristan"
    public partial class ProyectoTristanContext : DbContext
    {
        public ProyectoTristanContext() { }

        public ProyectoTristanContext(DbContextOptions<ProyectoTristanContext> options)
            : base(options) { }

        // Definición de DbSets para las entidades del modelo
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<DetallePedido> DetallePedidos { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        // Configuración del modelo mediante Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A1072F3DDFD");

                entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            // Configuración de la entidad DetallePedido
            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasKey(e => e.IdDetallePedido).HasName("PK__DetalleP__48AFFD95F39A95DB");

                entity.ToTable("DetallePedido");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                // Configuración de la relación con Pedido
                entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK__DetallePe__IdPed__571DF1D5");

                // Configuración de la relación con Producto
                entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetallePe__IdPro__5812160E");
            });

            // Configuración de la entidad Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__9D335DC3DACDA1C5");

                entity.ToTable("Pedido");

                entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                // Configuración de la relación con Usuario
                entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Pedido__IdUsuari__534D60F1");
            });

            // Configuración de la entidad Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto).HasName("PK__Producto__098892100E52E671");

                entity.ToTable("Producto");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
                entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Imagen).IsUnicode(false);
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.PrecioOferta).HasColumnType("decimal(10, 2)");

                // Configuración de la relación con Categoria
                entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Producto__IdCate__4CA06362");
            });

            // Configuración de la entidad Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97EA544500");

                entity.ToTable("Usuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        // Método parcial para permitir la extensión de OnModelCreating en archivos parciales
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
