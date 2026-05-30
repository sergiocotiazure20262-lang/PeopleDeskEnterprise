using Microsoft.EntityFrameworkCore;
using PeopleDesk.Domain.Interfaces.Repositories;
using PeopleDesk.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
           where TEntity : class
    {
        protected readonly PeopleDeskDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(PeopleDeskDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<List<TEntity>> ObterTodosAsync(
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task AdicionarAsync(
            TEntity entidade,
            CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entidade, cancellationToken);
        }

        public virtual void Atualizar(TEntity entidade)
        {
            DbSet.Update(entidade);
        }

        public virtual void Remover(TEntity entidade)
        {
            DbSet.Remove(entidade);
        }
    }
}
