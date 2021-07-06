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
        protected readonly ContasDbContext Db;
        protected readonly DbSet<TEntity> DbSet;
        public Repository(ContasDbContext context)
        {
            Db = context;
            DbSet = context.Set<TEntity>();
        }

        public void Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            Db.SaveChanges();
        }

        public void Atualizar(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Excluir(int Id)
        {
            var entity = DbSet.Find(Id);
            DbSet.Remove(entity);
            Db.SaveChanges();
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public TEntity ObterPorId(int id)
        {
            return DbSet.Find(id);
        }
    }
}