using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using TaskManager.Shared.Common.Interfaces.Services;
using TaskManager.Shared.Common.Models;
using TaskManager.Shared.DataAccess.Abstraction;
using TaskManager.Shared.DataAccess.Abstraction.Repositories;

namespace TaskManager.BuisinessLogic.Services
{
    public abstract class GenericSearchWithIncludesService<TDTO, TEntity, TRepository> : GenericService<TDTO, TEntity, TRepository>
        where TDTO : class
        where TEntity : class
        where TRepository : IGenericRepository<TEntity>
    {
        public GenericSearchWithIncludesService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }


        protected IServiceResult<UDTO> FindWithIncludes<UDTO>(Expression<Func<TEntity,object>>[] includes, 
            params Expression<Func<UDTO, bool>>[] predicates)
            where UDTO : class
        {
            using (_unitOfWork)
            {
                var mappedPredicate = MapPredicates(predicates);
                var data = _unitOfWork.GetRepository<TRepository>().SearchWithIncludes(includes, mappedPredicate).FirstOrDefault();
                return new ServiceResult<UDTO>(_mapper.Map<UDTO>(data));
            }
            
        }

        protected IServiceResult<IEnumerable<UDTO>> SearchWithInclude<UDTO>(Expression<Func<TEntity, object>>[] includes,
            params Expression<Func<UDTO, bool>>[] predicates)
            where UDTO : class
        {
            using (_unitOfWork)
            {
                var mappedPredicate = MapPredicates(predicates);
                var data = _unitOfWork.GetRepository<TRepository>().SearchWithIncludes(includes, mappedPredicate).ToList();
                var dataResult = data.Select(x => _mapper.Map<UDTO>(x)).ToList();
                return new ServiceResult<IEnumerable<UDTO>>(dataResult);
            }
            
        }
    }
}
