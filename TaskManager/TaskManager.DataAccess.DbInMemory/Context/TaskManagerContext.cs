using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataAccess.Abstraction.DBOs;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;

namespace TaskManager.DataAccess.MsSql.Context
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
            : base(options)
        {
        }


        public DbSet<Task> Tasks { get; set; }
        public DbSet<SectionType> SectionTypes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Section> Sections { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Section>()
                .HasOne(s => s.Table)
                .WithMany(t => t.Sections)
                .HasForeignKey(s => s.TableId);

            modelBuilder.Entity<Section>()
                .HasOne(s => s.SectionType)
                .WithMany(st => st.Sections)
                .HasForeignKey(s => s.SectionTypeId);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.Section)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.SectionId);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.Table)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.TableId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<SectionType>()
                .HasData(new SectionType() { SectionTypeId = 1,SectionTypeName = "TODO", Order = 10,IsDefault = true },
                    new SectionType() { SectionTypeId = 2,SectionTypeName = "DOING", Order = 20, IsDefault = true },
                    new SectionType() { SectionTypeId = 3,SectionTypeName = "DONE", Order = 30, IsDefault = true });

        }
    }
}
