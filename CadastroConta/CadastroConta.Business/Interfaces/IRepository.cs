using CadastroConta.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroConta.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        void Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        void Excluir(int Id);
        TEntity ObterPorId(int id);
        Task<List<TEntity>> ObterTodos();
    }
}
