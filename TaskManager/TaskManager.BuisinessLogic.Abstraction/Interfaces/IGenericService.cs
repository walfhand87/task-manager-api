using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TaskManager.Shared.Common.Interfaces.Services;

namespace TaskManager.BuisinessLogic.Abstraction.Interfaces
{
    public interface IGenericService<TDTO> where TDTO:class
    {
        IServiceResult<TDTO> Find(params Expression<Func<TDTO, bool>>[] predicates);

        IServiceResult<IEnumerable<TDTO>> Search(params Expression<Func<TDTO, bool>>[] predicates);

        IServiceResult<TDTO> Insert(TDTO dto);

        IServiceResult<TDTO> Update(TDTO dto);

        IServiceResult Delete(object id);
    }
}
