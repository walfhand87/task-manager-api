using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.DataAccess.Abstraction
{
    public interface IDbContextContainer
    {
        DbContext CurrentDbContext { get; }
    }
}
