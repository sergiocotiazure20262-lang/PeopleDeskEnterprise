using PeopleDesk.Domain.Interfaces.Repositories;
using PeopleDesk.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PeopleDeskDbContext _context;

        public UnitOfWork(PeopleDeskDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
