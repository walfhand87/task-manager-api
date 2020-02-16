using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.Common.DTOs.Details
{
    public class SectionDetailsDTO : SectionDTO
    {
        public TableDTO Table { get; set; }
        public SectionTypeDTO SectionType { get; set; }
        public IEnumerable<TaskDTO> Tasks { get; set; }
    }
}
