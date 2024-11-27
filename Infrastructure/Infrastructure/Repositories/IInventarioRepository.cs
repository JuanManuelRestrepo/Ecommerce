using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IInventarioRepository
    {
        Task<IList<Inventario>> GetAllInventarios();
        Task<Inventario> GetInventarioById(Guid id);
        Task CreateInventario(Inventario inventario);
        Task UpdateInventario(Inventario inventario);
        Task DeleteInventarioById(Guid id);
    }
}
