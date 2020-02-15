using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DataAccess.Abstraction.DBOs.Basics
{
    public class Section
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int TableId { get; set; }
        public int SectionTypeId { get; set; }
        public Table Table { get; set; }
        public SectionType SectionType { get; set;}
        public IEnumerable<Task> Tasks { get; set; }
    }
}
