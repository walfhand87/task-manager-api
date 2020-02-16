using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DataAccess.Abstraction.DBOs;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;
using TaskManager.DataAccess.Abstraction.Interfaces.Repositories;
using TaskManager.Shared.DataAccess.Abstraction;
using TaskManager.Shared.DataAccess.EF.Repositories;

namespace TaskManager.DataAccess.MsSql.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(IDbContextContainer dbContextContainer) : base(dbContextContainer)
        {
        }
    }
}
