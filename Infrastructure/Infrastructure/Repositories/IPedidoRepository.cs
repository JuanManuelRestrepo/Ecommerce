using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPedidoRepository
    {
        // Obtener todos los pedidos
        Task<IList<Pedido>> GetAllPedidosAsync();

        // Obtener un pedido por ID
        Task<Pedido> GetPedidoByIdAsync(Guid id);

        // Crear un nuevo pedido
        Task CreatePedidoAsync(Pedido pedido);

        // Actualizar un pedido
        Task UpdatePedidoAsync(Pedido pedido);
        Task<List<Pedido>> GetPedidoUsuarioById(Guid idusuario);

        // Eliminar un pedido por ID
        Task DeletePedidoAsync(Guid id);

        // Eliminar un pedido por objeto
        Task DeletePedidoAsync(Pedido pedido);
    }
}
