using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PagoRepository :BaseRepository, IPagoRepository
    {
     

        public PagoRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<Pago> GetPagoById(Guid id)
        {
            return await context.Pagos // Asegúrate de que el DbSet se llame "Pagos"
                .Include(p => p.Pedido) // Incluye la relación con Pedido si es necesario
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pago>> GetAllPagos()
        {
            return await context.Pagos // Asegúrate de que el DbSet se llame "Pagos"
                .Include(p => p.Pedido) // Incluye la relación con Pedido si es necesario
                .ToListAsync();
        }

        // Método para verificar si existe un pago con el PedidoId.
        public async Task<bool> ExistePagoPorIdAsync(Guid pedidoId)
        {
            return await context.Pagos
                                 .AnyAsync(p => p.PedidoId == pedidoId); // Verifica si hay algún pago con ese PedidoId.
        }


        public async Task CreatePago(Pago pago)
        {
            if (pago == null) throw new ArgumentNullException(nameof(pago));

            await context.Pagos.AddAsync(pago); // Asegúrate de que el DbSet se llame "Pagos"
            await context.SaveChangesAsync();
        }

        public async Task UpdatePago(Pago pago)
        {
            if (pago == null) throw new ArgumentNullException(nameof(pago));

            context.Pagos.Update(pago); // Asegúrate de que el DbSet se llame "Pagos"
            await context.SaveChangesAsync();
        }

        public async Task DeletePago(Guid id)
        {
            var pago = await GetPagoById(id);
            if (pago == null) throw new ArgumentException($"No se encontró el pago con ID {id}");

            context.Pagos.Remove(pago); // Asegúrate de que el DbSet se llame "Pagos"
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pago>> GetPagosByPedidoId(Guid pedidoId)
        {
            return await context.Pagos // Asegúrate de que el DbSet se llame "Pagos"
                .Where(p => p.PedidoId == pedidoId)
                .ToListAsync();
        }
    }
}
