using Domain;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        // Obtener un pedido por ID
        public async Task<Pedido> GetPedidoById(Guid id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
            {
                throw new ArgumentException($"Pedido con ID {id} no encontrado.");
            }

            return pedido;
        }

        // Obtener todos los pedidos
        public async Task<IEnumerable<Pedido>> GetAllPedidos()
        {
            return await _pedidoRepository.GetAllPedidosAsync();
        }

        // Crear un nuevo pedido
        public async Task CreatePedido(Pedido pedido)
        {
            if (pedido == null)
                throw new ArgumentNullException(nameof(pedido), "El pedido no puede ser nulo.");

            if (pedido.Total <= 0)
                throw new ArgumentException("El total del pedido debe ser mayor que cero.");

            if (pedido.FechaPedido == DateTime.MinValue)
                throw new ArgumentException("La fecha del pedido es inválida.");

            if (pedido.UsuarioId == Guid.Empty)
                throw new ArgumentException("El ID del usuario es inválido.");

            // Crear el pedido utilizando el repositorio
            await _pedidoRepository.CreatePedidoAsync(pedido);
        }

        // Actualizar un pedido existente
        public async Task UpdatePedido(Guid id, Pedido pedido)
        {
            var existingPedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (existingPedido == null)
                throw new ArgumentException($"No se encontró el pedido con ID {id}.");

            if (pedido == null)
                throw new ArgumentNullException(nameof(pedido), "El pedido no puede ser nulo.");

            if (pedido.Total <= 0)
                throw new ArgumentException("El total del pedido debe ser mayor que cero.");

            if (pedido.FechaPedido == DateTime.MinValue)
                throw new ArgumentException("La fecha del pedido es inválida.");

            if (pedido.UsuarioId == Guid.Empty)
                throw new ArgumentException("El ID del usuario es inválido.");

            // Actualizar los datos del pedido
            existingPedido.Total = pedido.Total;
            existingPedido.FechaPedido = pedido.FechaPedido;
            existingPedido.UsuarioId = pedido.UsuarioId;
            existingPedido.Estado = pedido.Estado;

            // Actualizar el pedido en la base de datos
            await _pedidoRepository.UpdatePedidoAsync(existingPedido);
        }

        public async Task<List<Pedido>> GetPedidoUsuarioId(Guid id)
        {
            return await _pedidoRepository.GetPedidoUsuarioById(id);
        }

        // Eliminar un pedido por ID
        public async Task DeletePedido(Guid id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
                throw new ArgumentException($"No se encontró el pedido con ID {id}.");

            // Eliminar el pedido
            await _pedidoRepository.DeletePedidoAsync(id);
        }

        
    }
}

