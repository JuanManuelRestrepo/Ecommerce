using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ProductoServices : IProductoServices
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProductoServices(IProductoRepository productoRepository, ICategoriaRepository categoriaRepository)
        {
            _productoRepository = productoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IList<Producto>> GetAllProductos()
        {
            return await _productoRepository.GetAllProductos();
        }

        public async Task<Producto> GetProductoById(Guid id)
        {
            var producto = await _productoRepository.GetProductoById(id);
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
            catch (DbUpdateException dbEx)
            {
                // Puedes acceder a los detalles internos de la excepción
                var innerExceptionMessage = dbEx.InnerException?.Message ?? "Sin detalles adicionales";
                throw new Exception($"Error al crear el producto en la base de datos. Detalles internos: {innerExceptionMessage}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el producto: {ex.Message}", ex);
            }
        }



        public async Task<List<Producto>> GetProductCategoriaById(Guid categoriaId)
        {
            try
            {
                // Buscar la categoría de manera asincrónica
                var categoria =  _categoriaRepository.GetCategoriaById(categoriaId);

                if (categoria == null)
                {
                    throw new KeyNotFoundException("No existe esta categoría.");
                }

                // Obtener los productos de la categoría
                var productos = await _productoRepository.GetProductosByCategoriaId(categoriaId);

                if (productos == null || productos.Count == 0)
                {
                    throw new KeyNotFoundException("No hay productos en esta categoría.");
                }

                return productos;
            }
            catch (KeyNotFoundException knfEx)
            {
                // Lanzar un error más específico
                throw new Exception(knfEx.Message);
            }
            catch (DbUpdateException dbEx)
            {
                // Manejar excepciones de base de datos
                throw new Exception("Error en la base de datos: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada
                throw new Exception("Error inesperado: " + ex.Message);
            }
        }


        public async Task UpdateProducto(Guid id, Producto productoDto)
        {
            if (productoDto == null)
            {
                throw new ArgumentNullException(nameof(productoDto), "El producto no puede ser nulo");
            }

            // Obtener el producto existente
            var productoExistente = await _productoRepository.GetProductoById(id);
            if (productoExistente == null)
            {
                throw new Exception("Producto no encontrado");
            }

            // Actualizar solo las propiedades que han cambiado
            productoExistente.Nombre = !string.IsNullOrEmpty(productoDto.Nombre) ? productoDto.Nombre : productoExistente.Nombre;
            productoExistente.Descripcion = !string.IsNullOrEmpty(productoDto.Descripcion) ? productoDto.Descripcion : productoExistente.Descripcion;
            productoExistente.Precio = productoDto.Precio != default ? productoDto.Precio : productoExistente.Precio;
            productoExistente.CantidadDisponible = productoDto.CantidadDisponible != default ? productoDto.CantidadDisponible : productoExistente.CantidadDisponible;

            // Actualizar relaciones, si es necesario
            if (productoDto.CategoriaId != Guid.Empty)
            {
                productoExistente.CategoriaId = productoDto.CategoriaId;
            }
            if (productoDto.ProveedorId != Guid.Empty)
            {
                productoExistente.ProveedorId = productoDto.ProveedorId;
            }

            // Llamar al repositorio para actualizar el producto
            await _productoRepository.UpdateProducto(productoExistente);
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

        public async Task<List<Producto>> GetProductosByCategoriaId(Guid categoriaId)
        {
            return await _productoRepository.GetProductosByCategoriaId(categoriaId);
        }

    }
}