 using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {

        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<IList<Usuario>> GetAllUsuariosAsync()
        {
            return await context.Usuarios.ToListAsync();
        }

        public  Usuario GetUsuarioByIdAsync(Guid id)
        {
            return  context.Usuarios.Find(id);
        }

        public void CreateUsuario(Usuario usuario)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void UpdateUsuario(Usuario usuario)
        {
            context.Entry(usuario).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteUsuario(Usuario usuario)
        {
            var usuarioExistente = context.Usuarios.Find(usuario.Id);
            if (usuarioExistente != null)
            {
                context.Usuarios.Remove(usuarioExistente);
                context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Usuario no encontrado.");
            }
        }

        public void DeleteUsuarioById(Guid id)
        {
            var usuarioExistente = context.Usuarios.Find(id);
            if (usuarioExistente != null)
            {
                context.Usuarios.Remove(usuarioExistente);
                context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Usuario no encontrado.");
            }
        }

        public async Task<Usuario> LoginUsuarioAsync(string email, string password)
        {
            // Busca el usuario en la base de datos por el email proporcionado
            var usuario = await context.Usuarios.SingleOrDefaultAsync(u => u.Email == email);
    
            // Verifica si el usuario existe y si la contraseña es correcta
            if (usuario == null || !VerifyPassword(password, usuario.Contraseña))
            {
                return null; // Devuelve null si no se encuentra el usuario o la contraseña es incorrecta
            }

            // Devuelve el usuario encontrado si las credenciales son correctas
            return usuario;
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
        return password == passwordHash; // Asegúrate de implementar la lógica de hashing aquí
        }


    }
}

