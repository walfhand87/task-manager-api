﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DataAccess.Abstraction.DBOs.Basics
{
    public class SectionType
    {
        public int SectionTypeId { get; set; }
        public string SectionTypeName { get; set; }
        public int Order { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<Section> Sections { get; set; }
    }
}
