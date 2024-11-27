using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories { 

    public interface ICategoriaRepository
    { 

        Task<IList<Categoria>> GetAllCategorias();
        Categoria GetCategoriaById(Guid id);
        Task CreateCategoria(Categoria categoria);
        void UpdateCategoria(Categoria categori);
        void DeleteCategoria(Guid id);
    }
  
}

