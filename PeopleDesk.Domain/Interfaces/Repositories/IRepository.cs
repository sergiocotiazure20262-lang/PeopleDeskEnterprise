using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<List<TEntity>> ObterTodosAsync(CancellationToken cancellationToken = default);

        Task AdicionarAsync(TEntity obj, CancellationToken cancellationToken = default);

        void Atualizar(TEntity obj);

        void Remover(TEntity obj);
    }
}
