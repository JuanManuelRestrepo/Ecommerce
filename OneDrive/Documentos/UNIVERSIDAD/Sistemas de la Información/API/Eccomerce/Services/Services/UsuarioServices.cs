using Domain;
using Infrastructure.Repositories;
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

        public void CreateUsuario(Usuario usuario)
        {
            if (!ContraseñaServices.SeguridadContraseña(usuario.Contraseña))
            {
                throw new Exception("La contraseña no cumple con los requisitos de seguridad");
            }

            usuario.Contraseña = ContraseñaServices.HashearContraseñaConSalt(usuario.Contraseña);
            _usuarioRepository.CreateUsuario(usuario);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            // Obtener el usuario existente de la base de datos
            var usuarioExistente =  _usuarioRepository.GetUsuarioByIdAsync(usuario.Id);

            if (usuarioExistente == null)
            {
                throw new Exception("El usuario no existe.");
            }

            // Si la contraseña no es nula ni vacía, verificar y actualizar
            if (!string.IsNullOrEmpty(usuario.Contraseña))
            {
                if (!ContraseñaServices.SeguridadContraseña(usuario.Contraseña))
                {
                    throw new Exception("La nueva contraseña no cumple con los requisitos de seguridad.");
                }

                // Aplicar hashing a la nueva contraseña, aunque sea diferente o no
                usuarioExistente.Contraseña = ContraseñaServices.HashearContraseñaConSalt(usuario.Contraseña);
            }

            // Actualizar otros campos si han cambiado
            if (usuario.Name != usuarioExistente.Name)
            {
                usuarioExistente.Name = usuario.Name;
            }

            if (usuario.Email != usuarioExistente.Email)
            {
                usuarioExistente.Email = usuario.Email;
            }

            // Actualizar el usuario en la base de datos
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
