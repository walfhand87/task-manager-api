using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TaskManager.Shared.DataAccess.Abstraction;

namespace TaskManager.DataAccess.MsSql
{
    public class DbContextContainer : IDbContextContainer
    {
        public DbContextContainer(DbContext context)
        {
            CurrentDbContext = context;
        }

        public DbContext CurrentDbContext { get; }
    }
}
