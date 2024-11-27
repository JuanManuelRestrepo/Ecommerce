using Domain.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PedidoRepository : BaseRepository, IPedidoRepository
    {
        public PedidoRepository(AppDbContext context) : base(context)
        {
        }

        // Obtener todos los pedidos
        public async Task<IList<Pedido>> GetAllPedidosAsync()
        {
            return await context.Set<Pedido>()
                                 .Include(p => p.Usuario)
                                 .Include(p => p.Pago)
                                 .ToListAsync();
        }

        // Obtener un pedido por ID
        public async Task<Pedido> GetPedidoByIdAsync(Guid id)
        {
            return await context.Set<Pedido>()
                                 .Include(p => p.Usuario)
                                 .Include(p => p.Pago)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<List<Pedido>> GetPedidoUsuarioById(Guid idusuario)
        {
            // Usamos WhereAsync para obtener todos los pedidos de un usuario
            return await context.Pedidos
                .Where(p => p.UsuarioId == idusuario)
                .ToListAsync();
        }
        // Crear un nuevo pedido
        public async Task CreatePedidoAsync(Pedido pedido)
        {
            await context.Set<Pedido>().AddAsync(pedido);
            await context.SaveChangesAsync();
        }

        // Actualizar un pedido
        public async Task UpdatePedidoAsync(Pedido pedido)
        {
            context.Set<Pedido>().Update(pedido);
            await context.SaveChangesAsync();
        }

        // Eliminar un pedido
        public async Task DeletePedidoAsync(Guid id)
        {
            var pedidoExistente = await GetPedidoByIdAsync(id);
            if (pedidoExistente != null)
            {
                context.Set<Pedido>().Remove(pedidoExistente);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Pedido no encontrado.");
            }
        }

        // Eliminar un pedido por objeto
        public async Task DeletePedidoAsync(Pedido pedido)
        {
            var pedidoExistente = await context.Set<Pedido>().FindAsync(pedido.Id);
            if (pedidoExistente != null)
            {
                context.Set<Pedido>().Remove(pedidoExistente);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Pedido no encontrado.");
            }
        }
    }
}
