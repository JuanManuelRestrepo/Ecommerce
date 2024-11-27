using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPagoServices
    {
        Task<Pago> GetPagoById(Guid id);
        Task<IEnumerable<Pago>> GetAllPagos();
        Task CreatePago(Pago pago);
        Task UpdatePago(Guid id, Pago pago);
        Task DeletePago(Guid id);
        Task<IEnumerable<Pago>> GetPagosByPedidoId(Guid pedidoId);
    }

}
