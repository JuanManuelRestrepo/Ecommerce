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
        [HttpPost("CreateUsuario")]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                // Validación de los datos del usuario
                if (usuarioDTO == null)
                {
                    return BadRequest("El objeto usuario no puede ser nulo.");
                }

                if (string.IsNullOrEmpty(usuarioDTO.Email) || string.IsNullOrEmpty(usuarioDTO.Contraseña))
                {
                    return BadRequest("El correo y la contraseña son obligatorios.");
                }

                // Verificar si el correo electrónico ya está registrado
                var existingUser = await _usuarioService.GetUsuarioByEmailAsync(usuarioDTO.Email);
                if (existingUser == true)
                {
                    return BadRequest("El correo electrónico ya está registrado.");
                }

                // Mapear el DTO a un objeto de usuario
                var usuario = _mapper.Map<Usuario>(usuarioDTO);

                // Crear el usuario
                await _usuarioService.CreateUsuario(usuario);

                // Responder con un mensaje de éxito
                return Ok("Usuario creado exitosamente.");
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        // PUT: api/usuario
        [HttpPut("UpdateUsuario/{id}")]
        public async Task<IActionResult> UpdateUsuario(Guid id, [FromBody] UsuarioDTO usuarioDto)
        {
            try
            {
                await _usuarioService.UpdateUsuario (id ,_mapper.Map<Usuario>(usuarioDto));
                return Ok("Usuario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el usuario: {ex.Message}");
            }
        }
            // DELETE: api/usuario/{id}
            [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(Guid id)
        {
            _usuarioService.DeleteUsuarioById(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var usuario = await _usuarioService.LoginUsuarioAsync(loginRequest.Email, loginRequest.Password);

            if (usuario == null)
            {
                // Si las credenciales son incorrectas, devuelve un JSON con un mensaje de error
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });
            }

            // Si el login es exitoso, devuelve un JSON con la información del usuario
            var response = new
            {
                mensaje = "Inicio Exitoso",
                usuarioid = usuario.Id,
                nombre = usuario.Email,
                token = "EjemploDeTokenJWT" // Aquí podrías agregar un token JWT si lo implementas
            };

            return Ok(response);
        }
    }
}

