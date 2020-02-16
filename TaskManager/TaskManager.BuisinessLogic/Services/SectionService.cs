using System;
using System.Collections.Generic;
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
    public class SectionService : GenericSearchWithIncludesService<SectionDetailsDTO, Section, ISectionRepository>, ISectionService
    {
        public SectionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private Expression<Func<Section, object>>[] IncludesBase => new Expression<Func<Section, object>>[]
        {
            s => s.Table,
            s => s.SectionType,
            s => s.Tasks,
        };
        public IServiceResult<SectionDetailsDTO> FindWithIncludes(params Expression<Func<SectionDetailsDTO, bool>>[] predicates)
        {
            return FindWithIncludes(IncludesBase, predicates);
        }

        public IServiceResult<IEnumerable<SectionDetailsDTO>> SearchWithInclude(params Expression<Func<SectionDetailsDTO, bool>>[] predicates)
        {
            return SearchWithInclude(IncludesBase, predicates);
        }

        public IServiceResult<SectionDetailsDTO> Update(int id, SectionDetailsDTO sectionDetailsDTO)
        {
            if(sectionDetailsDTO.SectionId <= 0)
            {
                if(id <= 0)
                {
                    return new ServiceResult<SectionDetailsDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                }
                sectionDetailsDTO.SectionId = id;
            }

            return base.Update(sectionDetailsDTO);
        }
    }
}
