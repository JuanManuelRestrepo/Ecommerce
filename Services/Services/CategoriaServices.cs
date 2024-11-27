using Domain;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoriaServices: ICategoriaServices
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaServices(ICategoriaRepository categoriaRepository) { 
        
            _categoriaRepository = categoriaRepository;
        }


        public async Task<IList<Categoria>> GetAllCategoria()
        {
            return await _categoriaRepository.GetAllCategorias();

        }
        public async Task<Categoria> GetCategoriaById(Guid id)
        {

            try
            {
                var categoria = _categoriaRepository.GetCategoriaById(id);

                return categoria;
            }

            catch (Exception ex) { 
            
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateCategoria(Categoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria), "La categoria no puede ser nula");

            try
            {
                await _categoriaRepository.CreateCategoria(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear la categoria: {ex.Message}");
            }
        }

        public async Task UpdateCategoria(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria), "El producto no puede ser nulo");
            }

            try
            {
                 _categoriaRepository.UpdateCategoria(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el producto: {ex.Message}");
            }
        }

        public async Task DeleteProductoById(Guid id)
        {
            var productoEliminar = _categoriaRepository.GetCategoriaById(id);

            if (productoEliminar == null)
            {
                throw new Exception("Producto no encontrado");
            }

            try
            {
                 _categoriaRepository.DeleteCategoria(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el producto: {ex.Message}");
            }
        }
    }








}

