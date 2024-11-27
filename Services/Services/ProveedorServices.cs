using Domain;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class ProveedorService : IProveedorServices
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        // Obtener todos los proveedores
        public async Task<IList<Proveedor>> GetAllProveedores()
        {
            return await _proveedorRepository.GetAllProveedor();
        }

        // Obtener proveedor por Id
        public async Task<Proveedor> GetProveedorById(Guid id)
        {
            var proveedor = await _proveedorRepository.GetProveedorById(id);
            if (proveedor == null)
            {
                throw new Exception("Proveedor no encontrado");
            }
            return proveedor;
        }

        // Crear un nuevo proveedor
        public async Task CreateProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                throw new ArgumentNullException(nameof(proveedor), "El proveedor no puede ser nulo");
            }

            try
            {
                await _proveedorRepository.CreateProveedor(proveedor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear un nuevo proveedor: {ex.Message}");
            }
        }

        // Actualizar proveedor
        public async Task UpdateProveedor(Guid id, Proveedor proveedor)
        {
            var proveedorExistente = await _proveedorRepository.GetProveedorById(id);
            if (proveedorExistente == null)
            {
                throw new Exception("El proveedor no existe");
            }

            if (proveedor == null)
            {
                throw new ArgumentNullException(nameof(proveedor), "El proveedor no puede ser nulo");
            }

            proveedorExistente.Nombre = proveedor.Nombre ?? proveedorExistente.Nombre;
            proveedorExistente.Telefono = proveedor.Telefono ?? proveedorExistente.Telefono;
            proveedorExistente.Direccion = proveedor.Direccion ?? proveedorExistente.Direccion;
            proveedorExistente.Correo = proveedor.Correo ?? proveedorExistente.Correo;

            await _proveedorRepository.UpdateProveedor(proveedorExistente);
        }

        // Eliminar proveedor por Id
        public async Task DeleteProveedorById(Guid id)
        {
            var proveedorEliminar = await _proveedorRepository.GetProveedorById(id);
            if (proveedorEliminar == null)
            {
                throw new Exception("Proveedor no encontrado");
            }

            try
            {
                await _proveedorRepository.DeleteProveedorById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el proveedor: {ex.Message}");
            }
        }
    }
}
