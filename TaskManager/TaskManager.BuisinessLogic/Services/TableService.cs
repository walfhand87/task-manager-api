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
    public class TableService : GenericService<TableDTO, Table, ITableRepository>, ITableService
    {
        private Expression<Func<Table, object>>[] includeExpression;
        public TableService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            includeExpression = new Expression<Func<Table, object>>[]
            {
                t => t.Sections
            };
        }

        public override IServiceResult<TableDTO> Insert(TableDTO dto)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                using (_unitOfWork)
                {
                    var tableDbo = _unitOfWork.GetRepository<ITableRepository>().Insert(_mapper.Map<Table>(dto));
                    var sectionTypes = _unitOfWork.GetRepository<ISectionTypeRepository>().Search();

                    if (!sectionTypes.Any())
                    {
                        return new ServiceResult<TableDTO>(Shared.Common.Enums.ResultStatus.ERROR);
                    }

                    foreach (SectionType sectionType in sectionTypes)
                    {
                        if (sectionType.IsDefault)
                        {
                            Section section = new Section()
                            {
                                SectionName = sectionType.SectionTypeName,
                                SectionTypeId = sectionType.SectionTypeId,
                                Table = tableDbo,
                            };

                            _unitOfWork.GetRepository<ISectionRepository>().Insert(section);
                        }
                    }

                    _unitOfWork.SaveChanges();
                    var tableDboWithIncludes = _unitOfWork.GetRepository<ITableRepository>()
                    .SearchWithIncludes(includeExpression,t => t.TableId == tableDbo.TableId).FirstOrDefault();

                    return new ServiceResult<TableDTO>(_mapper.Map<TableDTO>(tableDboWithIncludes));
                }
            });
    
        }
    }
