using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.Common.DTOs
{
    public class TableDTO
    {
        public int TableId { get; set; }
        public bool Locked { get; set; }

        public IEnumerable<SectionDTO> Sections { get; set; }
        public IEnumerable<TaskDTO> Tasks { get; set; }
    }
}
