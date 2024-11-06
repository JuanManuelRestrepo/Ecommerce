using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Services
{
    public class ProductoServices : IProductoServices
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoServices(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IList<Producto>> GetAllProdutos()
        {
            return await _productoRepository.GetAllProductos();
        }

        public Task <Producto> GetProductoById(Guid id)
        {
            var producto =  _productoRepository.GetProductoById(id);
            if (producto == null)
            {
                throw new Exception("Producto no encontrado");
            }
            return producto;
        }

        public async Task CreateProducto(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser nulo");
            }

            try
            {
                await _productoRepository.CreateProducto(producto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el producto: {ex.Message}");
            }
        }

        public async Task UpdateProducto(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser nulo");
            }

            try
            {
                await _productoRepository.UpdateProducto(producto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el producto: {ex.Message}");
            }
        }

        public async Task DeleteProductoById(Guid id)
        {
            var productoEliminar = await _productoRepository.GetProductoById(id);

            if (productoEliminar == null)
            {
                throw new Exception("Producto no encontrado");
            }

            try
            {
                await _productoRepository.DeleteProductoByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el producto: {ex.Message}");
            }
        }
    }
}
