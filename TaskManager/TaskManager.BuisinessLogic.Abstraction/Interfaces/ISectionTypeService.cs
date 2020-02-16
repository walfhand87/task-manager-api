using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Shared.Common.DTOs;
using TaskManager.Shared.Common.DTOs.Details;
using TaskManager.Shared.Common.Interfaces.Services;

namespace TaskManager.BuisinessLogic.Abstraction.Interfaces
{
    public interface ISectionTypeService: IGenericService<SectionTypeDetailsDTO>, IGenericSearchWithIncludesService<SectionTypeDetailsDTO>
    {
        IServiceResult<SectionTypeDetailsDTO> Update(int id, SectionTypeDetailsDTO sectionDetailsDTO);
    }
}
