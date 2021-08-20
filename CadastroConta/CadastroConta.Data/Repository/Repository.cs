using CadastroConta.Business.Interfaces;
using CadastroConta.Business.Models;
using CadastroConta.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroConta.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly ContasDbContext _db;
        protected readonly DbSet<TEntity> _dbSet;
        public Repository(ContasDbContext context)
        {
            _db = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Adicionar(TEntity entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }

        public void Atualizar(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Excluir(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            _db.SaveChanges();
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public TEntity ObterPorId(int id)
        {
            return _dbSet.Find(id);
        }
    }
}