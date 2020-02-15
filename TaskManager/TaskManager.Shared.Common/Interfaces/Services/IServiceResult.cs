using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Shared.Common.Enums;

namespace TaskManager.Shared.Common.Interfaces.Services
{
    public interface IServiceResult
    {
        ResultStatus Status { get; set; }
        string ErrorCode { get; set; }
    }

    public interface IServiceResult<T> : IServiceResult
    {
        T Payload { get; set; }
    }
}
