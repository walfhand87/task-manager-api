using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.Common.DTOs.Details
{
    public class SectionTypeDetailsDTO : SectionTypeDTO
    {
        public IEnumerable<SectionDTO> Sections { get; set; }
    }
}
