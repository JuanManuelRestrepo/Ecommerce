using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IProveedorRepository
    {
        Task<IList<Proveedor>> GetAllProveedor();
        Task<Proveedor> GetProveedorById(Guid id);
        Task CreateProveedor(Proveedor proveedor);
        Task<List<Proveedor>> GetProveedoresById(Guid id);
        Task UpdateProveedor(Proveedor proveedor);
        Task DeleteProveedorById(Guid id);
    }
}
