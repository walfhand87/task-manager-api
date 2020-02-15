using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DataAccess.Abstraction.DBOs.Basics
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SectionId { get; set; }
        public int TableId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? EndDate { get; set; }

        public Table Table { get; set; }
        public Section Section { get; set; }
    }
}
