using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IMapper _mapper;

        public PedidoController(IPedidoService pedidoService, IMapper mapper)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
        }

        // Obtener todos los pedidos
        [HttpGet]
        public async Task<IActionResult> GetAllPedidos()
        {
            var pedidos = await _pedidoService.GetAllPedidos();
            var pedidosDto = _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);
            return Ok(pedidosDto);
        }

        // Obtener un pedido por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(Guid id)
        {
            try
            {
                var pedido = await _pedidoService.GetPedidoById(id);
                var pedidoDto = _mapper.Map<PedidoDTO>(pedido);
                return Ok(pedidoDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/Pedidos/{usuarioId}
        [HttpGet("GetPedidoUsuarioID")]
        public async Task<ActionResult<List<Pedido>>> GetPedidosPorUsuarioId(Guid usuarioId)
        {
            // Llamar al servicio para obtener los pedidos
            var pedidos = await _pedidoService.GetPedidoUsuarioId(usuarioId);

            if (pedidos == null )
            {
                return NotFound(new { mensaje = "No se encontraron pedidos para este usuario." });
            }

            return Ok(_mapper.Map<List<PedidoDTO>>(pedidos));
        }

        // Crear un nuevo pedido
        [HttpPost]
        public async Task<IActionResult> CreatePedido([FromBody] PedidoDTO pedidoDto)
        {
            if (pedidoDto == null)
            {
                return BadRequest("El pedido es inválido.");
            }

            try
            {
                var pedido = _mapper.Map<Pedido>(pedidoDto);
                await _pedidoService.CreatePedido(pedido);
                return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.Id }, pedidoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Actualizar un pedido existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(Guid id, [FromBody] PedidoDTO pedidoDto)
        {
            if (pedidoDto == null)
            {
                return BadRequest("El pedido es inválido.");
            }

            try
            {
                var pedido = _mapper.Map<Pedido>(pedidoDto);
                await _pedidoService.UpdatePedido(id, pedido);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


    

        // Eliminar un pedido
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(Guid id)
        {
            try
            {
                await _pedidoService.DeletePedido(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

      
        
    }
}
