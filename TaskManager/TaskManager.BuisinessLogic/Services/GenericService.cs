using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskManager.BuisinessLogic.Abstraction.Interfaces;
using TaskManager.Shared.Common.Enums;
using TaskManager.Shared.Common.Interfaces.Services;
using TaskManager.Shared.Common.Models;
using TaskManager.Shared.DataAccess.Abstraction;
using TaskManager.Shared.DataAccess.Abstraction.Repositories;

namespace TaskManager.BuisinessLogic.Services
{
    public abstract class GenericService<TDTO, TEntity, TRepository> : IGenericService<TDTO>
        where TDTO : class
        where TEntity : class
        where TRepository : IGenericRepository<TEntity>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public GenericService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public virtual IServiceResult Delete(object id)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                using (_unitOfWork)
                {
                    var entity = _unitOfWork.GetRepository<TRepository>().Find(id);
                    if (entity == null)
                    {
                        return new ServiceResult(ResultStatus.NO_RESULT);
                    }

                    _unitOfWork.GetRepository<TRepository>().Delete(entity);
                    int result = _unitOfWork.SaveChanges();
                    bool success = result > 0;
                    return new ServiceResult(success ? ResultStatus.SUCCESS : ResultStatus.NOT_MODIFIED);
                }

            });

        public virtual IServiceResult<TDTO> Find(params Expression<Func<TDTO, bool>>[] predicates)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                using (_unitOfWork)
                {
                    var data = _unitOfWork.GetRepository<TRepository>().Find(MapPredicates(predicates));
                    if (data == null)
                    {
                        return new ServiceResult<TDTO>(ResultStatus.NO_RESULT);
                    }
                    return new ServiceResult<TDTO>(_mapper.Map<TDTO>(data));
                }

            });

        public virtual IServiceResult<TDTO> Insert(TDTO dto)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                using (_unitOfWork)
                {
                    var entity = _mapper.Map<TEntity>(dto);
                    _unitOfWork.GetRepository<TRepository>().Insert(entity);
                    int result = _unitOfWork.SaveChanges();
                    var resDto = _mapper.Map<TDTO>(entity);
                    bool succes = result > 0;
                    return new ServiceResult<TDTO>(resDto, succes ? ResultStatus.SUCCESS : ResultStatus.ERROR);
                }

            });

        public virtual IServiceResult<IEnumerable<TDTO>> Search(params Expression<Func<TDTO, bool>>[] predicates)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                using (_unitOfWork)
                {
                    var mappedPredicate = MapPredicates(predicates);
                    var query = _unitOfWork.GetRepository<TRepository>().Search(mappedPredicate).Select(x => _mapper.Map<TDTO>(x));
                    var result = query.ToList();
                    if (!result.Any())
                    {
                        return new ServiceResult<IEnumerable<TDTO>>(result, ResultStatus.NO_RESULT);
                    }
                    return new ServiceResult<IEnumerable<TDTO>>(result);
                }

            });

        public virtual IServiceResult<TDTO> Update(TDTO dto)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                using (_unitOfWork)
                {
                    var entity = _mapper.Map<TEntity>(dto);
                    _unitOfWork.GetRepository<TRepository>().Update(entity);

                    int result = _unitOfWork.SaveChanges();
                    var resDto = _mapper.Map<TDTO>(entity);
                    bool succes = result > 0;

                    return new ServiceResult<TDTO>(resDto, succes ? ResultStatus.SUCCESS : ResultStatus.ERROR);
                }

            });

        public virtual IServiceResult<TDTO> Find(object id)
            => ExecuteAndHandleCommonExceptions(() =>
            {
                using (_unitOfWork)
                {
                    var entity =_unitOfWork.GetRepository<TRepository>().Find(id);
                    if(entity == null)
                    {
                        return new ServiceResult<TDTO>(ResultStatus.NO_RESULT);
                    }

                    return new ServiceResult<TDTO>(_mapper.Map<TDTO>(entity));
                }
            });


        protected Expression<Func<TEntity, bool>>[] MapPredicates<UDTO>(Expression<Func<UDTO, bool>>[] predicates)
        {
            return predicates.Select(_mapper.Map<Expression<Func<TEntity, bool>>>).ToArray();
        }


        protected IServiceResult ExecuteAndHandleCommonExceptions(Func<IServiceResult> functionToExecute)
        {
            try
            {
                return functionToExecute();
            }
            catch (Exception e)
            {
                return new ServiceResult(ResultStatus.ERROR);
            }
        }

        protected IServiceResult<UDTO> ExecuteAndHandleCommonExceptions<UDTO>(Func<IServiceResult<UDTO>> functionToExecute)
        {
            try
            {
                return functionToExecute();
            }
            catch (Exception e)
            {
                return new ServiceResult<UDTO>(ResultStatus.ERROR);
            }
        }

    }
}
