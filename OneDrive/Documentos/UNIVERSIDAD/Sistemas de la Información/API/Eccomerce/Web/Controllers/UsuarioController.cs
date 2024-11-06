using ApiSampleFinal.Models.DTO;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ApiSampleFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioService;
        private readonly IMapper _mapper;   

        public UsuarioController(IUsuarioServices usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper =mapper ;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(Guid id)
        {
            var usuario =  _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return BadRequest();
            }
            
            return Ok(usuario);
        }

        // POST: api/usuario
        [HttpPost ("CreateUsuario")]
        public IActionResult CreateUsuario([FromBody] UsuarioDTO usuario)
        {
            
            _usuarioService.CreateUsuario(_mapper.Map<Usuario>(usuario));
            return Ok("usuario Creado ");
        }

        // PUT: api/usuario
        [HttpPut]
        public IActionResult UpdateUsuario([FromBody] UsuarioDTO usuario)
        {
            _usuarioService.UpdateUsuario(_mapper.Map<Usuario>(usuario));
            return Ok("Usuario Actualizado");
        }

        // DELETE: api/usuario/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(Guid id)
        {
            _usuarioService.DeleteUsuarioById(id);
            return NoContent();
        }

        // POST: api/usuario/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var usuario = await _usuarioService.LoginUsuarioAsync(loginRequest.Email, loginRequest.Password);
            if (usuario == null) return Unauthorized();
            return Ok("Inicio Exitoso");
        }
    }
}
