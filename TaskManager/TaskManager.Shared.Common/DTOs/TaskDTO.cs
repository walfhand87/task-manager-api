using System;

namespace TaskManager.Shared.Common.DTOs
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SectionId { get; set; }
        public int TableId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? EndDate { get; set; }

        public TableDTO Table { get; set; }
        public SectionDTO Section { get; set; }
    }
}
