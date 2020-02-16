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
using TaskManager.Shared.Common.DTOs.Details;
using TaskManager.Shared.Common.Interfaces.Services;
using TaskManager.Shared.Common.Models;
using TaskManager.Shared.DataAccess.Abstraction;

namespace TaskManager.BuisinessLogic.Services
{
    public class TaskService : GenericSearchWithIncludesService<TaskDetailsDTO, Task, ITaskRepository>, ITaskService
    {
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        private Expression<Func<Task, object>>[] IncludesBase => new Expression<Func<Task, object>>[]
        {
            t => t.Table,
            t => t.Section
        };
        public override IServiceResult<TaskDetailsDTO> Insert(TaskDetailsDTO dto)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                using (_unitOfWork)
                {
                    var table = _unitOfWork.GetRepository<ITableRepository>()
                    .SearchWithIncludes(new Expression<Func<Table, object>>[] { t => t.Sections.Select(s => s.SectionType)},t => t.TableId == dto.TableId).FirstOrDefault();

                    if(table == null || table.Locked)
                    {
                        return new ServiceResult<TaskDetailsDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                    }

                    var sectionToCreateTask = table.Sections.OrderBy(s => s.SectionType.Order).FirstOrDefault();

                    if(sectionToCreateTask == null)
                    {
                        return new ServiceResult<TaskDetailsDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                    }

                    dto.SectionId = sectionToCreateTask.SectionId;

                    return base.Insert(dto);
                }
                
            });

        public IServiceResult<TaskDetailsDTO> FindWithIncludes(params Expression<Func<TaskDetailsDTO, bool>>[] predicates)
        {
            return FindWithIncludes(IncludesBase, predicates);
        }

        public IServiceResult<IEnumerable<TaskDetailsDTO>> SearchWithInclude(params Expression<Func<TaskDetailsDTO, bool>>[] predicates)
        {
            return SearchWithInclude(IncludesBase, predicates);
        }

        public IServiceResult<TaskDetailsDTO> Update(int id, TaskDetailsDTO taskDTO)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                if (taskDTO.TaskId <= 0)
                {
                    if (id <= 0)
                    {
                        return new ServiceResult<TaskDetailsDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                    }
                    taskDTO.TaskId = id;
                }

                using (_unitOfWork)
                {
                    var taskDbo = _unitOfWork.GetRepository<ITaskRepository>()
                    .SearchWithIncludes(new Expression<Func<Task, object>>[] { t => t.Section.SectionType }, t => t.TaskId == taskDTO.TaskId)
                    .FirstOrDefault();

                    if (taskDbo == null || taskDbo.Section?.SectionType == null)
                    {
                        return new ServiceResult<TaskDetailsDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                    }

                    if(taskDbo.TableId != taskDTO.TableId)
                    {
                        var newTable = _unitOfWork.GetRepository<ITableRepository>().Find(taskDTO.TableId);
                        if(newTable == null || newTable.Locked)
                        {
                            return new ServiceResult<TaskDetailsDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                        }
                    }

                    var sections = _unitOfWork.GetRepository<ISectionRepository>()
                    .SearchWithIncludes(new Expression<Func<Section, object>>[] { s => s.SectionType },
                        s => s.TableId == taskDbo.TableId)
                    .OrderBy(s => s.SectionType.Order).ToList();

                    var sectionWanted = sections.FirstOrDefault(s => s.SectionId == taskDTO.SectionId);
                    var currentSection = sections.FirstOrDefault(s => s.SectionId == taskDbo.SectionId);
                    
                    if(sectionWanted == null)
                    {
                        return new ServiceResult<TaskDetailsDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                    }

                    var checkSectionResult = CheckSectionToGo(taskDTO, sections, currentSection, sectionWanted);
                    if (checkSectionResult.Status != Shared.Common.Enums.ResultStatus.SUCCESS)
                    {
                        return checkSectionResult;
                    }

                    return base.Update(taskDTO);
                }
            });


        private IServiceResult<TaskDetailsDTO> CheckSectionToGo(TaskDetailsDTO taskDTO,IEnumerable<Section> sections,Section currentSection, Section wantedSection)
        {
            List<Section> possibeSectionsToGo = new List<Section>();
            for (int i = 0; i < sections.Count(); i++)
            {
                if (sections.ElementAt(i)?.SectionId == currentSection.SectionId)
                {
                    if (i - 1 >= 0)
                    {
                        possibeSectionsToGo.Add(sections.ElementAt(i - 1));
                    }

                    if (i + 1 <= sections.Count() - 1)
                    {
                        possibeSectionsToGo.Add(sections.ElementAt(i + 1));
                    }
                }
            }

            possibeSectionsToGo = possibeSectionsToGo.Where(s => s != null).ToList();

            if (possibeSectionsToGo.FirstOrDefault(s => s.SectionId == wantedSection.SectionId) != null
            || currentSection.SectionId == wantedSection.SectionId)
            {
                taskDTO.SectionId = wantedSection.SectionId;
                return new ServiceResult<TaskDetailsDTO>(taskDTO);
            }

            return new ServiceResult<TaskDetailsDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);

        }
    }
}
