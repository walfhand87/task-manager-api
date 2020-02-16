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
    public class SectionTypeController : BaseController
    {
        private readonly ISectionTypeService _sectionTypeService;

        public SectionTypeController(ISectionTypeService sectionTypeService)
        {
            _sectionTypeService = sectionTypeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SectionTypeDetailsDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Get()
        {
            var result = _sectionTypeService.SearchWithInclude();
            return GenerateResult(result);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SectionTypeDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Get(int id)
        {
            var result = _sectionTypeService.FindWithIncludes(s => s.SectionTypeId == id);
            return GenerateResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SectionTypeDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Post([FromBody] SectionTypeDetailsDTO tableDTO)
        {
            var result = _sectionTypeService.Insert(tableDTO);
            return GenerateResult(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(SectionTypeDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        public IActionResult Put(int id, [FromBody] SectionTypeDetailsDTO tableDTO)
        {
            var result = _sectionTypeService.Update(id,tableDTO);
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