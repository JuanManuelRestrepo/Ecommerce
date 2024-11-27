using Domain;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos { get; set; }
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

        // Configuración de relaciones

        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Usuario)
            .WithOne()
            .HasForeignKey<Pedido>(p => p.UsuarioId);

        // Pedido - Pago
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Pago)
            .WithOne(p => p.Pedido)
            .HasForeignKey<Pago>(p => p.PedidoId);

        // Producto - Inventario
        modelBuilder.Entity<Inventario>()
            .HasOne(i => i.Producto)
            .WithMany(p => p.Inventarios)
            .HasForeignKey(i => i.ProductoId);

        // Producto - Proveedor
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Proveedor)
            .WithMany(prov => prov.Productos)
            .HasForeignKey(p => p.ProveedorId);

        // Producto - Categoria
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId);

        // Configuración de propiedades decimales
        modelBuilder.Entity<Pedido>()
            .Property(p => p.Total)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Pago>()
            .Property(p => p.Monto)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasColumnType("decimal(18,2)");

        // Datos iniciales para la tabla Rol
        modelBuilder.Entity<Rol>().HasData(
            new Rol { RolName = "Administrador" },
            new Rol { RolName = "Usuario" }
        );
    }
}
