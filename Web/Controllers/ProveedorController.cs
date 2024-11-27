using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;
using Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorServices _proveedorServices;
        private readonly IMapper _mapper;

        public ProveedorController(IProveedorServices proveedorServices, IMapper mapper)
        {
            _proveedorServices = proveedorServices;
            _mapper = mapper;
        }

        // Obtener todos los proveedores
        [HttpGet]
        public async Task<IActionResult> GetAllProveedores()
        {
            try
            {
                var proveedores = await _proveedorServices.GetAllProveedores();
                var proveedoresDTO = _mapper.Map<List<ProveedorDTO>>(proveedores);

                return Ok(proveedoresDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al obtener los proveedores: {ex.Message}" });
            }
        }

        // Obtener proveedor por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProveedorById(Guid id)
        {
            try
            {
                var proveedor = await _proveedorServices.GetProveedorById(id);
                var proveedorDTO = _mapper.Map<ProveedorDTO>(proveedor);

                return Ok(proveedorDTO);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"Proveedor no encontrado: {ex.Message}" });
            }
        }

        // Crear un nuevo proveedor
        [HttpPost]
        public async Task<IActionResult> CreateProveedor([FromBody] ProveedorDTO proveedorDTO)
        {
            if (proveedorDTO == null)
            {
                return BadRequest("Proveedor no puede ser nulo");
            }

            var proveedor = _mapper.Map<Proveedor>(proveedorDTO);

            try
            {
                await _proveedorServices.CreateProveedor(proveedor);
                return CreatedAtAction(nameof(GetProveedorById), new { id = proveedor.Id }, proveedorDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al crear el proveedor: {ex.Message}" });
            }
        }

        // Actualizar proveedor
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProveedor(Guid id, [FromBody] ProveedorDTO proveedorDTO)
        {
            if (proveedorDTO == null)
            {
                return BadRequest("Proveedor no puede ser nulo");
            }

            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorDTO);
                proveedor.Id = id; // Aseguramos que el Id del proveedor sea el correcto

                await _proveedorServices.UpdateProveedor(id, proveedor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"Error al actualizar el proveedor: {ex.Message}" });
            }
        }

        // Eliminar proveedor
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(Guid id)
        {
            try
            {
                await _proveedorServices.DeleteProveedorById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"Error al eliminar el proveedor: {ex.Message}" });
            }
        }
    }
}
