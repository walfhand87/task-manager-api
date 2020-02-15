﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using TaskManager.BuisinessLogic.Abstraction.Interfaces;
using TaskManager.Shared.Common.DTOs;
using TaskManager.Shared.Common.Enums;
using TaskManager.Shared.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : BaseController
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TableDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Get()
        {
            var result = _tableService.Search();
            return GenerateResult(result);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TableDTO),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Get(int id)
        {
            var result = _tableService.Find(id);
            return GenerateResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TableDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Post([FromBody] TableDTO tableDTO)
        {
            var result = _tableService.Insert(tableDTO);
            return GenerateResult(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(TableDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Put(int id, [FromBody] TableDTO tableDTO)
        {
            var result = _tableService.Update(tableDTO);
            return GenerateResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Delete(int id)
        {
            var result = _tableService.Delete(id);
            return GenerateResult(result);
        }
    }
}