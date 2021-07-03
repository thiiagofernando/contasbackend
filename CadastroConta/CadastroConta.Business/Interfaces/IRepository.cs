using CadastroConta.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroConta.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        Task<List<TEntity>> ObterTodos();
        Task<int> SaveChanges();
    }
}
