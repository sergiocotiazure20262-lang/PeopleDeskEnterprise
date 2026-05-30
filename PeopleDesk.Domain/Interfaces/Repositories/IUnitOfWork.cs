using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
