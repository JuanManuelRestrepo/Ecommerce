using Domain;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UsuarioServices: IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IList<Usuario>> GetAllUsuariosAsync()
        {
            return await _usuarioRepository.GetAllUsuariosAsync();
        }

        public Usuario GetUsuarioByIdAsync(Guid id)
        {
            return  _usuarioRepository.GetUsuarioByIdAsync(id);
        }

        public async Task<bool> GetUsuarioByEmailAsync(string email)
        {
            // Buscar un usuario en la base de datos por el correo electrónico
            return await _usuarioRepository.EmailExistsAsync(email);
        }

       

        public async Task CreateUsuario(Usuario usuario)
        {
            // Validar si el correo ya existe en la base de datos
            bool emailExistente = await _usuarioRepository.EmailExistsAsync(usuario.Email);
            if (emailExistente)
            {
                throw new Exception("El correo ya está registrado.");
            }

            // Validar la seguridad de la contraseña
            if (!ContraseñaServices.SeguridadContraseña(usuario.Contraseña))
            {
                throw new Exception("La contraseña no cumple con los requisitos de seguridad.");
            }

            // Hashear la contraseña antes de guardar
            usuario.Contraseña = ContraseñaServices.HashearContraseñaConSalt(usuario.Contraseña);

            // Crear el usuario en la base de datos
             _usuarioRepository.CreateUsuario(usuario);
        }


        public async Task UpdateUsuario(Guid id, Usuario usuarioDto)
        {
            if (usuarioDto == null)
            {
                throw new ArgumentNullException(nameof(usuarioDto), "El usuario no puede ser nulo");
            }

            // Obtener el usuario existente
            var usuarioExistente =  _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuarioExistente == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            // Verificar si el correo ya está registrado
            bool emailExistente = await _usuarioRepository.EmailExistsAsync(usuarioDto.Email);
            if (emailExistente && usuarioDto.Email != usuarioExistente.Email)
            {
                throw new Exception("El correo ya está registrado.");
            }

            // Validar la contraseña si es proporcionada
            if (!string.IsNullOrEmpty(usuarioDto.Contraseña) && !ContraseñaServices.SeguridadContraseña(usuarioDto.Contraseña))
            {
                throw new Exception("La contraseña no cumple con los requisitos de seguridad.");
            }

            // Actualizar solo las propiedades que han cambiado
            usuarioExistente.Name = !string.IsNullOrEmpty(usuarioDto.Name) ? usuarioDto.Name : usuarioExistente.Name;
            usuarioExistente.Email = !string.IsNullOrEmpty(usuarioDto.Email) ? usuarioDto.Email : usuarioExistente.Email;
            usuarioExistente.Direccion = !string.IsNullOrEmpty(usuarioDto.Direccion) ? usuarioDto.Direccion : usuarioExistente.Direccion;
            usuarioExistente.Telefono = usuarioDto.Telefono != 0 ? usuarioDto.Telefono : usuarioExistente.Telefono;


            // Si se proporciona una nueva contraseña, actualizarla
            if (!string.IsNullOrEmpty(usuarioDto.Contraseña))
            {
                usuarioExistente.Contraseña = ContraseñaServices.HashearContraseñaConSalt(usuarioDto.Contraseña);
            }

            // Llamar al repositorio para actualizar el usuario
            _usuarioRepository.UpdateUsuario(usuarioExistente);
        }
    


    public void DeleteUsuario(Usuario usuario)
        {
            _usuarioRepository.DeleteUsuario(usuario);
        }

        public void DeleteUsuarioById(Guid id)
        {
            _usuarioRepository.DeleteUsuarioById(id);
        }

        public async Task<Usuario> LoginUsuarioAsync(string email, string password)
        {

            var contraseñaEncriptada = ContraseñaServices.HashearContraseñaConSalt(password);
            Console.WriteLine(contraseñaEncriptada);
            return await _usuarioRepository.LoginUsuarioAsync(email, contraseñaEncriptada);
        }


        private bool VerifyPassword(string password, string passwordHash)
        {
            // Implementa la lógica de verificación de la contraseña
            return password == passwordHash; // Asegúrate de implementar la lógica de hashing aquí
        }

    }
}
