﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DataAccess.Abstraction.DBOs.Basics
{
    public class Table
    {
        public int TableId { get; set; }
        public bool Locked { get; set; }
        public IEnumerable<Section> Sections { get; set; }
    }
}