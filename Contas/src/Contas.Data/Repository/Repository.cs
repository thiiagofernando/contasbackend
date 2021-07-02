using Contas.Business.Interfaces;
using Contas.Business.Models;
using Contas.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contas.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly ContasDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbSet<TEntity> dbSet)
        {
            DbSet = dbSet;
        }

        public Repository(ContasDbContext context)
        {
            Db = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
