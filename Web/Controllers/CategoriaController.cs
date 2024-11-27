namespace Eccomerce.Controllers
{
    using AutoMapper;
    using Domain;
    using Eccomerce.Models.DTO;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System;
    using System.Threading.Tasks;

    namespace API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CategoriaController : ControllerBase
        {
            private readonly ICategoriaServices _categoriaServices ;
            private readonly IMapper _mapper;

            public CategoriaController(ICategoriaServices categoriaservices, IMapper mapper)
            {
                _categoriaServices = categoriaservices;
                _mapper = mapper;

            }
                [HttpGet("GetAllCategoria")]
            public async Task<IActionResult> GetAllCategorias()
            {
                try
                {
                    var Categorias = await _categoriaServices.GetAllCategoria(); // Corrección: método correcto
                    var categoriasDTO = _mapper.Map<IList<CategoriaDTO>>(Categorias);
                    return Ok(categoriasDTO);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener productos: {ex.Message}");
                }
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetCategoriaById(Guid id)
            {
                var categoria = await _categoriaServices.GetCategoriaById(id);
                if (categoria == null)
                    return NotFound();

                return Ok(categoria);
            }
            [HttpPost("CreateCategoria")]
            public async Task<IActionResult> CreateCategoria([FromBody] CategoriaDTO categoria)
            {
                if (categoria == null)
                    return BadRequest("La categoría es nula.");

                try
                {
                    await _categoriaServices.CreateCategoria(_mapper.Map<Categoria>(categoria));
                    return CreatedAtAction(nameof(GetCategoriaById), new { id = categoria.Nombre }, categoria);
                }
                catch (Exception ex)
                {
                    // Devuelve detalles sobre el error para facilitar la depuración
                    return StatusCode(500, $"Error al crear la categoría: {ex.Message}");
                }
            }

            [HttpPut("UpdateCategoria")]
            public async Task<IActionResult> UpdateCategoria(Guid id, [FromBody] CategoriaDTO categoria)
            {
                if (categoria == null || categoria.id != id)
                    return BadRequest();

                await _categoriaServices.UpdateCategoria(_mapper.Map<Categoria>(categoria));
                return NoContent();
            }

            [HttpDelete("Deletecategoria")]
            public async Task<IActionResult> DeleteCategoria(Guid id)
            {
                await _categoriaServices.DeleteProductoById(id);
                return NoContent();
            }
        }
    }

}
