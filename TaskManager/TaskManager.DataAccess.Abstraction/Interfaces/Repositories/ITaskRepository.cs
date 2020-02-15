using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DataAccess.Abstraction.DBOs;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;
using TaskManager.Shared.DataAccess.Abstraction.Repositories;

namespace TaskManager.DataAccess.Abstraction.Interfaces.Repositories
{
    public interface ITaskRepository: IGenericRepository<Task>
    {
    }
}
