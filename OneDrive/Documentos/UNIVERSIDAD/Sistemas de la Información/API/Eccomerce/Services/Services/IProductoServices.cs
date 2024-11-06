using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductoServices
    {
        Task<IList<Producto>> GetAllProdutos();
        Task<Producto> GetProductoById(Guid id);
        Task CreateProducto(Producto producto);
        Task UpdateProducto(Producto producto);
        Task DeleteProductoById(Guid id);
    }
}
