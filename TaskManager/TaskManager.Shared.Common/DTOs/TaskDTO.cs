using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.Common.DTOs
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public int Title { get; set; }
        public int SectionId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
