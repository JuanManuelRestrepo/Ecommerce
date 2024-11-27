using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductoServices
    {
        Task<IList<Producto>> GetAllProductos();
        Task<Producto> GetProductoById(Guid id);
        Task CreateProducto(Producto producto);
        Task<List<Producto>> GetProductosByCategoriaId(Guid categoriaId);
        Task UpdateProducto(Guid id, Producto producto);
        Task DeleteProductoById(Guid id);
    }
}
