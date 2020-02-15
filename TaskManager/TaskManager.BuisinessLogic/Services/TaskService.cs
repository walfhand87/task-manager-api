using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using TaskManager.BuisinessLogic.Abstraction.Interfaces;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;
using TaskManager.DataAccess.Abstraction.Interfaces.Repositories;
using TaskManager.Shared.Common.DTOs;
using TaskManager.Shared.Common.Interfaces.Services;
using TaskManager.Shared.Common.Models;
using TaskManager.Shared.DataAccess.Abstraction;

namespace TaskManager.BuisinessLogic.Services
{
    public class TaskService : GenericService<TaskDTO, Task, ITaskRepository>, ITaskService
    {
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }


        public override IServiceResult<TaskDTO> Insert(TaskDTO dto)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                using (_unitOfWork)
                {
                    var table = _unitOfWork.GetRepository<ITableRepository>()
                    .SearchWithIncludes(new Expression<Func<Table, object>>[] { t => t.Sections.Select(s => s.SectionType)},t => t.TableId == dto.TableId).FirstOrDefault();

                    if(table == null || table.Locked)
                    {
                        return new ServiceResult<TaskDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                    }

                    var sectionToCreateTask = table.Sections.OrderBy(s => s.SectionType.Order).FirstOrDefault();

                    if(sectionToCreateTask == null)
                    {
                        return new ServiceResult<TaskDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                    }

                    dto.SectionId = sectionToCreateTask.SectionId;

                    return base.Insert(dto);
                }
                
            });
    }
}
