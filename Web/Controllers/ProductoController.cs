using Microsoft.AspNetCore.Mvc;
using Services;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Eccomerce.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApiSampleFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _productoService;
        private readonly IMapper _mapper;

        public ProductoController(IProductoServices productoService, IMapper mapper)
        {
            _productoService = productoService;
            _mapper = mapper;
        }

        // GET: api/producto
        [HttpGet]
        public async Task<IActionResult> GetAllProductos()
        {
            try
            {
                var productos = await _productoService.GetAllProductos(); // Corrección: nombre del método
                var productosDto = _mapper.Map<IList<ProductoDTO>>(productos);
                return Ok(productosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener productos: {ex.Message}");
            }
        }

        // GET: api/producto/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoById(Guid id)
        {
            try
            {
                var producto = await _productoService.GetProductoById(id);
                if (producto == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado");
                }
                return Ok(_mapper.Map<ProductoDTO>(producto));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener producto: {ex.Message}");
            }
        }


        [HttpGet("GetproductoCategoria")]
        public async Task<IActionResult> GetProductoCategoriaById(Guid categoriaId)
        {
            try
            {
                // Verificar si el id es Guid.Empty
                if (categoriaId == Guid.Empty)
                {
                    return BadRequest("Id de categoría no válido.");
                }

                var productos = await _productoService.GetProductosByCategoriaId(categoriaId);

                if (productos == null || productos.Count == 0)
                {
                    // Si no se encuentran productos, se devuelve un NotFound en lugar de Ok
                    return NotFound("No se encontraron productos en esta categoría.");
                }

                // Mapear los productos a ProductoDTO antes de devolverlos
                var productosDto = _mapper.Map<List<ProductoDTO>>(productos);
                return Ok(productosDto);
            }
            catch (Exception ex)
            {
                // Enviar mensaje de error detallado en caso de excepción
                return BadRequest($"Error: {ex.Message}");
            }
        }



        [HttpPost("Create")]
        public async Task<IActionResult> CreateProducto([FromBody] ProductoDTO productoDto)
        {
            if (productoDto == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            try
            {
                // Mapeamos el DTO a la entidad Producto
                var productoEntity = new Producto
                {
                    Id = Guid.NewGuid(), // Asignamos un nuevo Id si es necesario
                    Nombre = productoDto.Nombre,
                    Descripcion = productoDto.Descripcion,
                    Precio = productoDto.Precio,
                    CantidadDisponible = productoDto.CantidadDisponible,
                    FechaCreacion = DateTime.Now,// Asignamos la fecha de creación automáticamente
                    ProveedorId = productoDto.ProveedorId,
                    CategoriaId = productoDto.idCategoria

                };

                // Guardamos el product
                await _productoService.CreateProducto(productoEntity);

                return CreatedAtAction(nameof(GetProductoById), new { id = productoEntity.Id }, productoDto);
            }
            catch (Exception ex)
            {
                // Log y manejo de error
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear el producto: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(Guid id, [FromBody] ProductoDTO productoDto)
        {
            // Validar que el producto no sea nulo
            if (productoDto == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            // Validar que el ID sea válido
            if (id == Guid.Empty)
            {
                return BadRequest("El ID del producto no es válido.");
            }

            try
            {
                // Mapear el DTO a la entidad del dominio
                var producto = _mapper.Map<Producto>(productoDto);

                // Validar que los valores sean positivos
                if (producto.Precio < 0 || producto.CantidadDisponible < 0)
                {
                    return BadRequest("El precio y la cantidad disponible deben ser valores positivos.");
                }

                // Llamar al servicio para actualizar el producto
                var productoActualizado =  _productoService.UpdateProducto(id, producto);

                // Verificar si el producto fue encontrado y actualizado
                if (productoActualizado == null)
                {
                    return NotFound("No se encontró el producto con el ID proporcionado.");
                }

                return Ok("Producto actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                // Manejar errores inesperados
                return StatusCode(500, $"Error del servidor: {ex.Message}");
            }
        }




        // DELETE: api/producto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(Guid id)
        {
            try
            {
                var producto = await _productoService.GetProductoById(id);
                if (producto == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado");
                }

                await _productoService.DeleteProductoById(id);
                return NoContent(); // Respuesta adecuada para eliminar
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el producto: {ex.Message}");
            }
        }
    }
}
