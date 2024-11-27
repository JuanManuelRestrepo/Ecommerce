using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
 
        public interface ICategoriaServices
        {
            Task<IList<Categoria>> GetAllCategoria();
            Task<Categoria> GetCategoriaById(Guid id);
            Task CreateCategoria(Categoria categoria);
            Task UpdateCategoria(Categoria categoria);
            Task DeleteProductoById(Guid id);
        }
    }

