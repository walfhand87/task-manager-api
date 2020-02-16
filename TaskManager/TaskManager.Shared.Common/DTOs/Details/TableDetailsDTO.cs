using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.Common.DTOs.Details
{
    public class TableDetailsDTO : TableDTO
    {
        public IEnumerable<SectionDTO> Sections { get; set; }
        public IEnumerable<TaskDTO> Tasks { get; set; }
    }
}
