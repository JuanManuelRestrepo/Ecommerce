using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUsuarioServices
    {
        Task<IList<Usuario>> GetAllUsuariosAsync();
        Usuario GetUsuarioByIdAsync(Guid id);
        Task CreateUsuario(Usuario usuario);
        Task UpdateUsuario(Guid id, Usuario usuario);
        void DeleteUsuario(Usuario usuario);
        void DeleteUsuarioById(Guid id);
        Task<bool> GetUsuarioByEmailAsync(string email);
   
        Task<Usuario> LoginUsuarioAsync(string email, string password);

    }
}
