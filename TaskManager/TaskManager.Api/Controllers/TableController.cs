using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.BuisinessLogic.Abstraction.Interfaces;
using TaskManager.DataAccess.Abstraction.Interfaces.Repositories;
using TaskManager.Shared.Common.Enums;
using TaskManager.Shared.DataAccess.Abstraction;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }
        public IActionResult Get()
        {
            var test = _tableService.Search();
            switch (test.Status)
            {
                case ResultStatus.SUCCESS:
                    return Ok(test.Payload);
                case ResultStatus.ERROR:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                case ResultStatus.NO_RESULT:
                    return NoContent();
                default:
                    return StatusCode((int)HttpStatusCode.NotImplemented);
            }
        }
    }
}