using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IPagoRepository
    {
        Task<Pago> GetPagoById(Guid id);
        Task<IEnumerable<Pago>> GetAllPagos();
        Task CreatePago(Pago pago);
        Task UpdatePago(Pago pago);
        Task DeletePago(Guid id);
        Task<IEnumerable<Pago>> GetPagosByPedidoId(Guid pedidoId);

        Task<bool> ExistePagoPorIdAsync(Guid pedidoId);
    }
}