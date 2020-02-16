using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.BuisinessLogic.Abstraction.Interfaces;
using TaskManager.Shared.Common.DTOs;
using TaskManager.Shared.Common.DTOs.Details;
using TaskManager.Shared.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskDetailsDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Get()
        {
            var result = _taskService.SearchWithInclude();
            return GenerateResult(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TaskDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Get(int id)
        {
            var result = _taskService.FindWithIncludes(x => x.TaskId == id);
            return GenerateResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TaskDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Post([FromBody] TaskDetailsDTO taskDTO)
        {
            var result = _taskService.Insert(taskDTO);
            return GenerateResult(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(TaskDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Put(int id, [FromBody] TaskDetailsDTO taskDTO)
        {
            var result = _taskService.Update(id,taskDTO);
            return GenerateResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Delete(int id)
        {
            var result = _taskService.Delete(id);
            return GenerateResult(result);
        }
    }
}
