using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly AppDbContext _context;

        public InventarioRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los inventarios
        public async Task<IList<Inventario>> GetAllInventarios()
        {
            try
            {
                return await _context.Inventarios
                    .Include(i => i.Producto) // Incluir el Producto relacionado
                    .ToListAsync();
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
                return await _context.Inventarios
                    .Include(i => i.Producto) // Incluir el Producto relacionado
                    .FirstOrDefaultAsync(i => i.Id == id);
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
                await _context.Inventarios.AddAsync(inventario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el inventario", ex);
            }
        }

        // Actualizar un inventario existente
        public async Task UpdateInventario(Inventario inventario)
        {
            try
            {
                _context.Inventarios.Update(inventario);
                await _context.SaveChangesAsync();
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
                var inventario = await _context.Inventarios.FindAsync(id);
                if (inventario != null)
                {
                    _context.Inventarios.Remove(inventario);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Inventario con ID {id} no encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el inventario con ID {id}", ex);
            }
        }
    }
}
