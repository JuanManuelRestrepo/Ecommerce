using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IProductoRepository
    {
        Task<IList<Producto>> GetAllProductos();
        Task<Producto> GetProductoById(Guid id);
        Task CreateProducto(Producto producto);
        Task<Producto> GetProductoByIdAsync(Guid id);
        Task<List<Producto>> GetProductCategoriaById(Guid id);
        Task<List<Producto>> GetProductosByCategoriaId(Guid categoriaId);
        Task UpdateProducto(Producto producto);
        Task DeleteProductoByID(Guid id);
    }
}
