using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.BuisinessLogic.Abstraction.Interfaces;
using TaskManager.Shared.Common.DTOs;
using TaskManager.Shared.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionTypeController : BaseController
    {
        private readonly ISectionTypeService _sectionTypeService;

        public SectionTypeController(ISectionTypeService sectionTypeService)
        {
            _sectionTypeService = sectionTypeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SectionTypeDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Get()
        {
            var result = _sectionTypeService.Search();
            return GenerateResult(result);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SectionTypeDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Get(int id)
        {
            var result = _sectionTypeService.Find(id);
            return GenerateResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SectionTypeDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Post([FromBody] SectionTypeDTO tableDTO)
        {
            var result = _sectionTypeService.Insert(tableDTO);
            return GenerateResult(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(SectionTypeDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Put(int id, [FromBody] SectionTypeDTO tableDTO)
        {
            var result = _sectionTypeService.Update(tableDTO);
            return GenerateResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Delete(int id)
        {
            var result = _sectionTypeService.Delete(id);
            return GenerateResult(result);
        }
    }

    

}