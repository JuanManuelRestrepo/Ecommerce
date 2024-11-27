using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class InventarioService : IInventarioService
    {
        private readonly IInventarioRepository _inventarioRepository;

        public InventarioService(IInventarioRepository inventarioRepository)
        {
            _inventarioRepository = inventarioRepository;
        }

        // Obtener todos los inventarios
        public async Task<IList<Inventario>> GetAllInventarios()
        {
            try
            {
                return await _inventarioRepository.GetAllInventarios();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los inventarios", ex);
            }
        }

        // Obtener inventario por ID
        public async Task<Inventario> GetInventarioById(Guid id)
        {
            try
            {
                var inventario = await _inventarioRepository.GetInventarioById(id);
                if (inventario == null)
                {
                    throw new Exception($"Inventario con ID {id} no encontrado");
                }
                return inventario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el inventario con ID {id}", ex);
            }
        }

        // Crear un nuevo inventario
        public async Task CreateInventario(Inventario inventario)
        {
            try
            {
                // Validar si los campos requeridos están vacíos o nulos
                if (inventario.ProductoId == Guid.Empty || inventario.Cantidad <= 0)
                {
                    throw new ArgumentException("ProductoId y cantidad son campos obligatorios y no pueden ser vacíos o nulos.");
                }

                // Validar que la fecha de entrada no sea en el futuro
                if (inventario.FechaEntrada > DateTime.Now)
                {
                    throw new ArgumentException("La fecha de entrada no puede ser en el futuro.");
                }

                await _inventarioRepository.CreateInventario(inventario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el inventario", ex);
            }
        }

        // Actualizar un inventario existente
        public async Task UpdateInventario(Guid id, Inventario inventario)
        {
            try
            {
                // Verificar si el inventario con el ID especificado existe
                var inventarioExistente = await _inventarioRepository.GetInventarioById(id);
                if (inventarioExistente == null)
                {
                    throw new Exception($"Inventario con ID {id} no encontrado para actualizar.");
                }

                // Validar si los campos requeridos están vacíos o nulos
                if (inventario.ProductoId == Guid.Empty || inventario.Cantidad <= 0)
                {
                    throw new ArgumentException("ProductoId y cantidad son campos obligatorios y no pueden ser vacíos o nulos.");
                }

                // Validar que la fecha de entrada no sea en el futuro
                if (inventario.FechaEntrada > DateTime.Now)
                {
                    throw new ArgumentException("La fecha de entrada no puede ser en el futuro.");
                }

                inventario.Id = id; // Asegúrate de que el Id coincida
                await _inventarioRepository.UpdateInventario(inventario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el inventario", ex);
            }
        }

        // Eliminar un inventario por ID
        public async Task DeleteInventarioById(Guid id)
        {
            try
            {
                // Verificar si el inventario con el ID especificado existe
                var inventario = await _inventarioRepository.GetInventarioById(id);
                if (inventario == null)
                {
                    throw new Exception($"Inventario con ID {id} no encontrado para eliminar.");
                }

                await _inventarioRepository.DeleteInventarioById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el inventario", ex);
            }
        }
    }
}
