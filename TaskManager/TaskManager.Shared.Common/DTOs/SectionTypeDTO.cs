using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.Common.DTOs
{
    public class SectionTypeDTO
    {
        public int SectionTypeId { get; set; }
        public string SectionTypeName { get; set; }
        public int Order { get; set; }
        public bool IsDefault { get; set; }

        public IEnumerable<SectionDTO> Sections { get; set; }
    }
}
