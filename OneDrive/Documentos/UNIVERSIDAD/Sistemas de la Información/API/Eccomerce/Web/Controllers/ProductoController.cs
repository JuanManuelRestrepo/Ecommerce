using Microsoft.AspNetCore.Mvc;
using Services;
using Domain;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Eccomerce.Models.DTO;
using System.Linq.Expressions;

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
        [HttpGet("GetAllProductos")]

        public async Task<IActionResult> GetAllProductos()
        {
            var productos = await _productoService.GetAllProdutos();
            var productosDto = _mapper.Map<IList<ProductoDTO>>(productos);
            return Ok(productosDto);
        }


        // GET: api/producto/{id}
        [HttpGet("GetProductoById")]
        public IActionResult GetProductoById(Guid id)
        {
            var producto = _productoService.GetProductoById(id);
            return Ok(_mapper.Map<ProductoDTO>(producto));
        }

        // POST: api/producto
        [HttpPost("CreateProducto")]
        public IActionResult CreateProducto([FromBody] ProductoDTO producto)
        {
            try { 
            _productoService.CreateProducto(_mapper.Map<Producto>(producto));
            return Ok("Producto creado"); } 

            
           catch (Exception ex)
            {
                throw new Exception($"Error al crear el producto: {ex.Message}");
    }
}

        // PUT: api/producto
        [HttpPut("UpdateProducto")]
        public IActionResult UpdateProducto([FromBody] ProductoDTO producto)
        {
            _productoService.UpdateProducto(_mapper.Map<Producto>(producto));
            return Ok("Producto actualizado");
        }

        // DELETE: api/producto/{id}
        [HttpDelete("DeleteProducto")]
        public IActionResult DeleteProducto(Guid id)
        {
            _productoService.DeleteProductoById(id);
            return NoContent();
        }
    }
}
