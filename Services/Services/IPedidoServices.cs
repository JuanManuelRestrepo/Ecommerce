using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPedidoService
    {
        Task<Pedido> GetPedidoById(Guid id);
        Task<IEnumerable<Pedido>> GetAllPedidos();
        Task<List<Pedido>> GetPedidoUsuarioId(Guid id);
        Task CreatePedido(Pedido pedido);
        Task UpdatePedido(Guid id, Pedido pedido);
        Task DeletePedido(Guid id);
      
    }
}
