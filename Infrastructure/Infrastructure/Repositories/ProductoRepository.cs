using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductoRepository : BaseRepository, IProductoRepository
    {
        public ProductoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Producto>> GetAllProductos()
        {
            return await context.Productos.ToListAsync();
        }

        public async Task<Producto> GetProductoById(Guid id)
        {
            return await context.Productos
                .Include(p => p.Proveedor)
                .Include(p => p.categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Producto> GetProductoByIdAsync(Guid id)
        {
            return await context.Productos.FindAsync(id);
        }



        public async Task CreateProducto(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }

            // Verificar si el Proveedor existe
            var proveedorExistente = await context.Proveedores
                .AnyAsync(p => p.Id == producto.ProveedorId);
            if (!proveedorExistente)
            {
                throw new Exception($"El proveedor con ID {producto.ProveedorId} no existe.");
            }

            // Verificar si la Categoria existe
            var categoriaExistente = await context.Categorias
                .AnyAsync(c => c.Id == producto.CategoriaId);
            if (!categoriaExistente)
            {
                throw new Exception($"La categoría con ID {producto.CategoriaId} no existe.");
            }

            try
            {
                await context.Productos.AddAsync(producto);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el producto: {ex.Message}", ex);
            }
        }


        public async Task<List<Producto>> GetProductosByCategoriaId(Guid categoriaId)
        {
            return await context.Productos
                .Where(p => p.CategoriaId == categoriaId)
                .Include(p => p.categoria)
                .ToListAsync();
        }


        public async Task<List<Producto>> GetProductCategoriaById(Guid id)
        {
            try
            {
                return await context.Productos
                                    .Where(p => p.CategoriaId == id)
                                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, loguea el error o haz algo en consecuencia.
                throw new Exception("Error al obtener los productos por categoría", ex);
            }
        }

        public async Task UpdateProducto(Producto producto)
        {
            // Marca la entidad como modificada
            context.Productos.Update(producto);
            await context.SaveChangesAsync();
        }
        public async Task DeleteProductoByID(Guid id)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var producto = await GetProductoById(id);

                if (producto != null)
                {
                    context.Productos.Remove(producto);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    await transaction.RollbackAsync();
                    throw new KeyNotFoundException("Producto no encontrado para el ID especificado.");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar el producto: " + ex.Message, ex);
            }
        }
    }
}
