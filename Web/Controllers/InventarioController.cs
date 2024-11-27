using AutoMapper;
using Domain;
using Eccomerce.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSampleFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;
        private readonly IMapper _mapper;

        public InventarioController(IInventarioService inventarioService, IMapper mapper)
        {
            _inventarioService = inventarioService;
            _mapper = mapper;
        }

        // GET: api/inventario
        [HttpGet]
        public async Task<IActionResult> GetAllInventarios()
        {
            try
            {
                var inventarios = await _inventarioService.GetAllInventarios();
                var inventariosDto = _mapper.Map<IList<InventarioDTO>>(inventarios);
                return Ok(inventariosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener inventarios: {ex.Message}");
            }
        }

        // GET: api/inventario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventarioById(Guid id)
        {
            try
            {
                var inventario = await _inventarioService.GetInventarioById(id);
                if (inventario == null)
                {
                    return NotFound($"Inventario con ID {id} no encontrado");
                }
                return Ok(_mapper.Map<InventarioDTO>(inventario));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener inventario: {ex.Message}");
            }
        }

        // POST: api/inventario/create
        [HttpPost("CreateInventario")]
        public async Task<IActionResult> CreateCategoria([FromBody] InventarioDTO inventarioDTO)
        {
            if (inventarioDTO == null)
                return BadRequest("La categoría es nula.");

            try
            {
                await _inventarioService.CreateInventario(_mapper.Map<Inventario>(inventarioDTO));
                return CreatedAtAction(nameof(GetAllInventarios), new { id = inventarioDTO.ProductoId }, inventarioDTO);
            }
            catch (Exception ex)
            {
                // Devuelve detalles sobre el error para facilitar la depuración
                return StatusCode(500, $"Error al crear la categoría: {ex.Message}");
            }
        }

        // PUT: api/inventario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventario(Guid id, [FromBody] InventarioDTO inventarioDto)
        {
            if (inventarioDto == null)
            {
                return BadRequest("El inventario no puede ser nulo");
            }

            try
            {
                var inventarioEntity = _mapper.Map<Domain.Inventario>(inventarioDto);
                await _inventarioService.UpdateInventario(id, inventarioEntity);
                return Ok("Inventario actualizado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el inventario: {ex.Message}");
            }
        }

        // DELETE: api/inventario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(Guid id)
        {
            try
            {
                var inventario = await _inventarioService.GetInventarioById(id);
                if (inventario == null)
                {
                    return NotFound($"Inventario con ID {id} no encontrado");
                }

                await _inventarioService.DeleteInventarioById(id);
                return NoContent(); // Respuesta adecuada para eliminar
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el inventario: {ex.Message}");
            }
        }
    }
}
