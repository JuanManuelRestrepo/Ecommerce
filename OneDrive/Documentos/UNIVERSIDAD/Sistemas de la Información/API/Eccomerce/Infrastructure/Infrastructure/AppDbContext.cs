using Domain;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<DetallePedido> DetallePedidos { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Inventario> Inventarios { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Relaciones
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.usuario)
            .WithMany()
            .HasForeignKey(p => p.UsuarioId);

        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Pago)
            .WithOne(p => p.Pedido)
            .HasForeignKey<Pago>(p => p.PedidoId);

        modelBuilder.Entity<DetallePedido>()
            .HasOne(dp => dp.Pedido)
            .WithMany(p => p.Detalles)
            .HasForeignKey(dp => dp.PedidoId);

        modelBuilder.Entity<DetallePedido>()
            .HasOne(dp => dp.Producto)
            .WithMany(p => p.DetallePedidos)
            .HasForeignKey(dp => dp.ProductoId);

        modelBuilder.Entity<Inventario>()
            .HasOne(i => i.Producto)
            .WithMany(p => p.Inventarios)
            .HasForeignKey(i => i.ProductoId);

        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Proveedor)
            .WithMany(prov => prov.Productos)
            .HasForeignKey(p => p.ProveedorId);

        modelBuilder.Entity<Producto>()
            .HasOne<Categoria>()
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId);

        // Configuraciones de propiedades decimal
        modelBuilder.Entity<Pedido>()
            .Property(p => p.Total)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<DetallePedido>()
            .Property(dp => dp.Precio)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Pago>()
            .Property(p => p.Monto)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasColumnType("decimal(18,2)");

        // Sembrar datos iniciales para Rol
        modelBuilder.Entity<Rol>().HasData(
            new Rol { RolName = "Administrador" },
            new Rol { RolName = "Usuario" }
        );
    }
}
