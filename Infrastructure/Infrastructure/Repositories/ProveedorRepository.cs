using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProveedorRepository : BaseRepository, IProveedorRepository
    {
        public ProveedorRepository(AppDbContext context) : base(context) { }

        public async Task<IList<Proveedor>> GetAllProveedor()
        {
            return await context.Proveedores.ToListAsync();
        }
    
        public async Task<Proveedor> GetProveedorById(Guid id)
        {
            return await context.Proveedores
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateProveedor(Proveedor proveedor)
        {
            await context.Proveedores.AddAsync(proveedor);
            await context.SaveChangesAsync();
        }

        public async Task<List<Proveedor>> GetProveedoresById(Guid id)
        {
            return await context.Proveedores
                                 .Where(p => p.Id == id)
                                 .Include(p => p.Productos) 
                                 .ToListAsync();
        }
        public async Task UpdateProveedor(Proveedor proveedor)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                context.Entry(proveedor).State = EntityState.Modified;
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteProveedorById(Guid id)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var proveedor = await GetProveedorById(id);

                if (proveedor != null)
                {
                    context.Proveedores.Remove(proveedor);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
