using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure.Repositories
{
    public  interface IUsuarioRepository
    {
        Task<IList<Usuario>> GetAllUsuariosAsync();
        Usuario GetUsuarioByIdAsync(Guid id);
        void CreateUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        void DeleteUsuario(Usuario usuario);
        void DeleteUsuarioById(Guid id);
        Task<Usuario> LoginUsuarioAsync(string email, string password);
    }
}
