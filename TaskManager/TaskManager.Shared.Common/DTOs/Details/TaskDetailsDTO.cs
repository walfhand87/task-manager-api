using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.Common.DTOs.Details
{
    public class TaskDetailsDTO : TaskDTO
    {
        public TableDTO Table { get; set; }
        public SectionDTO Section { get; set; }
    }
}
