using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return context.Productos.Find(id);
        }




        public async Task CreateProducto(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser nulo");
            }

            try
            {
                await context.Productos.AddAsync(producto);  // Añadir el producto
                await context.SaveChangesAsync();            // Guardar cambios
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el producto: {ex.Message}", ex);  // Incluir la causa original
            }
        }







        public async Task UpdateProducto(Producto producto)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var ProductoExistente = await GetProductoById(producto.Id);
                if (ProductoExistente != null)
                {
                    // Actualizamos las propiedades directamente
                    ProductoExistente.Nombre = producto.Nombre ?? ProductoExistente.Nombre;
                    ProductoExistente.Descripcion = producto.Descripcion ?? ProductoExistente.Descripcion;
                    ProductoExistente.Precio = producto.Precio != default ? producto.Precio : ProductoExistente.Precio;
                    ProductoExistente.CantidadDisponible = producto.CantidadDisponible != default ? producto.CantidadDisponible : ProductoExistente.CantidadDisponible;
                    

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    throw new Exception("No existe la tarea");
                }
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task DeleteProductoByID(Guid id)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var tarea = await GetProductoById(id);

                if (tarea != null)
                {
                    context.Productos.Remove(tarea);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();


                }
                else
                {
                    await transaction.RollbackAsync();

                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

            }

        }
    }
}
