using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IInventarioService
    {
        Task<IList<Inventario>> GetAllInventarios();
        Task<Inventario> GetInventarioById(Guid id);
        Task CreateInventario(Inventario inventario);
        Task UpdateInventario(Guid id, Inventario inventario);
        Task DeleteInventarioById(Guid id);
    }
}
