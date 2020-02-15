using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.DataAccess.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        TRepository GetRepository<TRepository>();
    }
}
