using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Shared.Common.Enums;
using TaskManager.Shared.Common.Interfaces.Services;

namespace TaskManager.Shared.Common.Models
{
    public class ServiceResult : IServiceResult
    {
        public ServiceResult(ResultStatus status = ResultStatus.SUCCESS,string errorCode = null)
        {
            Status = status;
            ErrorCode = errorCode;
        }
        public ServiceResult(IServiceResult serviceResult)
            :this(serviceResult.Status,serviceResult.ErrorCode)
        {}
        public ResultStatus Status { get; set; }
        public string ErrorCode { get; set ; }
    }

    public class ServiceResult<T> : ServiceResult, IServiceResult, IServiceResult<T>
    {
        public ServiceResult(T payload,ResultStatus status = ResultStatus.SUCCESS,string errorCode = null)
            :base(status,errorCode)
        {
            Payload = payload;
        }
        public ServiceResult(ResultStatus status = ResultStatus.SUCCESS, string errorCode = null)
            :base(status,errorCode)
        {
            Payload = default;
        }

        public ServiceResult(IServiceResult serviceResult)
            :base(serviceResult.Status,serviceResult.ErrorCode)
        {
            Payload = default;
        }
        public T Payload { get; set; }
    }
}
