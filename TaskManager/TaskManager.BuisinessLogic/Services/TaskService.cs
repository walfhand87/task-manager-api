using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskManager.BuisinessLogic.Abstraction.Interfaces;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;
using TaskManager.DataAccess.Abstraction.Interfaces.Repositories;
using TaskManager.Shared.Common.DTOs;
using TaskManager.Shared.DataAccess.Abstraction;

namespace TaskManager.BuisinessLogic.Services
{
    public class TaskService : GenericService<TaskDTO, Task, ITaskRepository>, ITaskService
    {
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
