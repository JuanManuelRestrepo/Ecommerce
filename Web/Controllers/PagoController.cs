using AutoMapper;
using Application.DTOs;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoServices _pagoService;
        private readonly IMapper _mapper;

        public PagoController(IPagoServices pagoService, IMapper mapper)
        {
            _pagoService = pagoService;
            _mapper = mapper;
        }

        // Crear un nuevo pago
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePago([FromBody] PagoDTO
            pagoDto)
        {
            if (pagoDto == null)
                return BadRequest("El pago no puede ser nulo.");

            try
            {
                // Convertir el DTO a la entidad de dominio usando AutoMapper
                var pago = _mapper.Map<Pago>(pagoDto);
                pago.Id = Guid.NewGuid(); // Generar nuevo Id para el pago

                await _pagoService.CreatePago(pago);
                return Ok(new { Message = "Pago creado exitosamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Actualizar un pago existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePago(Guid id, [FromBody] PagoDTO pagoDto)
        {
            if (pagoDto == null)
                return BadRequest("El pago no puede ser nulo.");

            try
            {
                // Convertir el DTO a la entidad de dominio usando AutoMapper
                var pago = _mapper.Map<Pago>(pagoDto);
                pago.Id = id;

                await _pagoService.UpdatePago(id, pago);
                return Ok(new { Message = "Pago actualizado exitosamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Eliminar un pago
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(Guid id)
        {
            try
            {
                await _pagoService.DeletePago(id);
                return NoContent(); // 204 No Content
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtener un pago por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPagoById(Guid id)
        {
            try
            {
                var pago = await _pagoService.GetPagoById(id);
                var pagoDto = _mapper.Map<PagoDTO>(pago); // Mapeo a DTO

                return Ok(pagoDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Obtener pagos por PedidoId
        [HttpGet("pedido/{pedidoId}")]
        public async Task<IActionResult> GetPagosByPedidoId(Guid pedidoId)
        {
            try
            {
                var pagos = await _pagoService.GetPagosByPedidoId(pedidoId);
                var pagosDto = _mapper.Map<List<PagoDTO>>(pagos); // Mapeo a lista de DTOs

                return Ok(pagosDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Obtener todos los pagos
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPagos()
        {
            try
            {
                var pagos = await _pagoService.GetAllPagos(); // Llamada al servicio para obtener todos los pagos
                var pagosDto = _mapper.Map<List<PagoDTO>>(pagos); // Mapeo de la lista de pagos a DTOs

                return Ok(pagosDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Si ocurre un error, devuelve un BadRequest con el mensaje
            }
        }
    }
}
