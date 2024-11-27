using Domain;
using Infrastructure.Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PagoService : IPagoServices
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }



        public async Task<Pago> GetPagoById(Guid id)
        {
            var pago = await _pagoRepository.GetPagoById(id);
            if (pago == null)
                throw new ArgumentException($"Pago con ID {id} no encontrado.");

            return pago;
        }

        public async Task<IEnumerable<Pago>> GetAllPagos()
        {
            return await _pagoRepository.GetAllPagos();
        }

        public async Task CreatePago(Pago pago)
        {
            if (pago == null)
                throw new ArgumentNullException(nameof(pago), "El pago no puede ser nulo.");

       
            if (pago.Monto <= 0)
                throw new ArgumentException("El monto debe ser mayor que cero.");

            if (pago.FechaPago == DateTime.MinValue)
                throw new ArgumentException("La fecha de pago es inválida.");

            if (pago.PedidoId == Guid.Empty)
                throw new ArgumentException("El ID del pedido es inválido.");

            bool relacionExistente = await _pagoRepository.ExistePagoPorIdAsync(pago.PedidoId);

            if (relacionExistente)
            {
                // Si ya existe, se lanza una excepción indicando que ya hay un pago para ese pedido.
                throw new ArgumentException("Ya hay un pago para ese pedido.");
            }
            else
            {
                // Si no existe, se crea el nuevo pago.
                await _pagoRepository.CreatePago(pago);
            }


        }

        public async Task UpdatePago(Guid id, Pago pago)
        {
            // Validación: verificar que el pago exista
            var existingPago = await _pagoRepository.GetPagoById(id);
            if (existingPago == null)
                throw new ArgumentException($"No se encontró el pago con ID {id}.");

            // Validación: verificar que los campos sean válidos
            if (pago == null)
                throw new ArgumentNullException(nameof(pago), "El pago no puede ser nulo.");

            if (pago.Monto <= 0)
                throw new ArgumentException("El monto debe ser mayor que cero.");

            if (pago.FechaPago == DateTime.MinValue)
                throw new ArgumentException("La fecha de pago es inválida.");

            if (pago.PedidoId == Guid.Empty)
                throw new ArgumentException("El ID del pedido es inválido.");

            // Actualizar los datos del pago
            existingPago.PedidoId = pago.PedidoId;
            existingPago.MetodoPago = pago.MetodoPago;
            existingPago.Monto = pago.Monto;
            existingPago.FechaPago = pago.FechaPago;

            // Guardar los cambios
            await _pagoRepository.UpdatePago(existingPago);
        }

        public async Task DeletePago(Guid id)
        {
          
            var pago = await _pagoRepository.GetPagoById(id);
            if (pago == null)
                throw new ArgumentException($"No se encontró el pago con ID {id}.");

            // Eliminar el pago
            await _pagoRepository.DeletePago(id);
        }

        public async Task<IEnumerable<Pago>> GetPagosByPedidoId(Guid pedidoId)
        {
          
            if (pedidoId == Guid.Empty)
                throw new ArgumentException("El ID del pedido es inválido.");

          
            return await _pagoRepository.GetPagosByPedidoId(pedidoId);
        }
    }
}
