using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoriaRepository: BaseRepository, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<IList<Categoria>> GetAllCategorias()
        {
            return await context.Categorias.ToListAsync();
        }

        public Categoria GetCategoriaById(Guid id)
        {
            return context.Categorias.Find(id);
        }
        public async Task CreateCategoria(Categoria categoria)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Categorias.AddAsync(categoria);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public void UpdateCategoria(Categoria categoria)
        {
            context.Entry(categoria).State = EntityState.Modified;
            context.SaveChanges();
        }


        public void DeleteCategoria(Guid id)
        {
            var Categoriaxistente = context.Categorias.Find(id);
            if (Categoriaxistente != null)
            {
                context.Categorias.Remove(Categoriaxistente);
                context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Usuario no encontrado.");
            }
        }

    
    }
}

