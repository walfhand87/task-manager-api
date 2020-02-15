using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DataAccess.Abstraction.DBOs;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;

namespace TaskManager.DataAccess.DbInMemory.Context
{
    public class TaskManagerContext: DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
            :base(options)
        {

        }


        public DbSet<Task> Tasks { get; set; }
        public DbSet<SectionType> SectionTypes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Section> Sections { get; set; }
    }
}
