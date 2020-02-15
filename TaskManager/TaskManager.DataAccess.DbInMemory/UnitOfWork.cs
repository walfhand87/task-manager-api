using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Shared.DataAccess.Abstraction;

namespace TaskManager.DataAccess.DbInMemory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextContainer _dbContextContainer;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(IDbContextContainer dbContextContainer, IServiceProvider serviceProvider)
        {
            _dbContextContainer = dbContextContainer;
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            _dbContextContainer.CurrentDbContext.Dispose();
        }

        public TRepository GetRepository<TRepository>()
        {
            return (TRepository)_serviceProvider.GetService(typeof(TRepository));
        }

        public int SaveChanges()
        {
            return _dbContextContainer.CurrentDbContext.SaveChanges();
        }
    }
}
