using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TaskManager.Shared.Common.Enums;
using TaskManager.Shared.Common.Interfaces.Services;

namespace TaskManager.Shared.Common.Models
{
    public class BaseController : ControllerBase
    {
        protected IActionResult GenerateResult<TDTO>(IServiceResult<TDTO> serviceResult)
        {
            switch (serviceResult.Status)
            {
                case ResultStatus.SUCCESS:
                    return Ok(serviceResult.Payload);
                case ResultStatus.ERROR:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                case ResultStatus.NO_RESULT:
                    return NoContent();
                case ResultStatus.NOT_VALID:
                    return BadRequest();
                default:
                    return StatusCode((int)HttpStatusCode.NotImplemented);
            }
        }

        protected IActionResult GenerateResult(IServiceResult serviceResult)
        {
            switch (serviceResult.Status)
            {
                case ResultStatus.SUCCESS:
                    return Ok();
                case ResultStatus.ERROR:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                case ResultStatus.NO_RESULT:
                    return NoContent();
                case ResultStatus.NOT_VALID:
                    return BadRequest();
                default:
                    return StatusCode((int)HttpStatusCode.NotImplemented);
            }
        }
    }
}
