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
    public class SectionTypeService : GenericSearchWithIncludesService<SectionTypeDetailsDTO, SectionType, ISectionTypeRepository>,ISectionTypeService
    {
        public SectionTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private Expression<Func<SectionType, object>>[] IncludesBase => new Expression<Func<SectionType, object>>[]
        {
            s => s.Sections
        };

        public IServiceResult<SectionTypeDetailsDTO> FindWithIncludes(params Expression<Func<SectionTypeDetailsDTO, bool>>[] predicates)
        {
            return FindWithIncludes(IncludesBase, predicates);
        }

        public IServiceResult<IEnumerable<SectionTypeDetailsDTO>> SearchWithInclude(params Expression<Func<SectionTypeDetailsDTO, bool>>[] predicates)
        {
            return SearchWithInclude(IncludesBase, predicates);
        }

        public IServiceResult<SectionTypeDetailsDTO> Update(int id, SectionTypeDetailsDTO sectionTypeDetailsDTO)
        {
            if (sectionTypeDetailsDTO.SectionTypeId <= 0)
            {
                if (id <= 0)
                {
                    return new ServiceResult<SectionTypeDetailsDTO>(Shared.Common.Enums.ResultStatus.NOT_VALID);
                }
                sectionTypeDetailsDTO.SectionTypeId = id;
            }

            return base.Update(sectionTypeDetailsDTO);
        }
    }
}
