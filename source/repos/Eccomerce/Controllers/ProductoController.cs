using Eccomerce.ConexionBD;
using Eccomerce.Modelo;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
namespace Eccomerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoRepository _productoRepository;  // Declarar el repositorio

        // Constructor con inyección de dependencias
        public ProductoController(ProductoRepository productoRepository)
        {
            _productoRepository = productoRepository ?? throw new ArgumentNullException(nameof(productoRepository)); // Asignar la instancia inyectada
        }


        [HttpPost("CrearProductos")]
        public async Task<ActionResult> InsertarProducto(string Name, string Descripcion, int precio, int CantidadDisponible)
        {
            try
            {
                var resultado = await _productoRepository.InsertarProducto(Name, Descripcion, precio, CantidadDisponible);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("ObtenerProductos")]
        public async Task<ActionResult<List<ObteProducDTO>>> ObteProductos()
        {
            try
            {
                var listaProductos = await _productoRepository.ObtenerProductos();

                // Verifica si la lista está vacía
                if (listaProductos == null || !listaProductos.Any())
                {
                    return NotFound("No se encontraron productos.");
                }

                return Ok(listaProductos); // Devuelve una respuesta 200 OK con la lista de productos
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}"); // Devuelve una respuesta 500 en caso de error
            }
        }


        [HttpPut("ActualizarProducto/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ModificarProductoModel productoDTO)
        {
            if (id <= 0)
            {
                return BadRequest("El ID del producto no es válido.");
            }

            // Llama al repositorio para realizar la actualización
            bool resultado = await _productoRepository.ActualizarProducto(id, productoDTO);

            if (resultado)
            {
                return Ok("Producto actualizado correctamente.");
            }
            else
            {
                return NotFound("Producto no encontrado.");
            }
        }

        [HttpDelete("EliminarProducto")] 
        public async Task<ActionResult> BorrarProductos(int id)
        {
            try
            {
                var resultado = await _productoRepository.BorrarProducto(id);
                if (resultado)
                {
                    return Ok("Producto eliminado correctamente");
                }
                else
                {
                    return BadRequest("No fue posible eliminar el producto");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}"); // Devuelve una respuesta 500 en caso de error
            }
        }
    



   


}
}