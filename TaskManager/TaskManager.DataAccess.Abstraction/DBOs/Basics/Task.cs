using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DataAccess.Abstraction.DBOs.Basics
{
    public class Task
    {
        public int TaskId { get; set; }
        public int Title { get; set; }
        public int SectionId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? EndDate { get; set; }

        public Section Section { get; set; }
    }
}
