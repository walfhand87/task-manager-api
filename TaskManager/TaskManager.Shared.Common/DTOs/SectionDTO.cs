using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.Common.DTOs
{
    public class SectionDTO
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int TableId { get; set; }
        public int SectionTypeId { get; set; }

        public TableDTO Table { get; set; }
        public SectionTypeDTO SectionType { get; set; }
        public IEnumerable<TaskDTO> Tasks { get; set; }
    }
}
