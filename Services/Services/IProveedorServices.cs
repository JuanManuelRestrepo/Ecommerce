using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IProveedorServices
    {
        Task<IList<Proveedor>> GetAllProveedores();
        Task<Proveedor> GetProveedorById(Guid id);
        Task CreateProveedor(Proveedor proveedor);
        Task UpdateProveedor(Guid id, Proveedor proveedor);
        Task DeleteProveedorById(Guid id);
    }
}
