using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.Core.Interfaces.Services;
using SMS.Core.Models.Consultants.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMS.Common.Utilities;
using SMS.Core.Models.Consultants.Response;
using SMS.Common.Models.Filter;
using SMS.Business.Filters;

namespace SMS.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConsultantsController : ControllerBase
	{
		private readonly IConsultantService consultantService;
		public ConsultantsController(IConsultantService consultantService)
		{
			this.consultantService = consultantService;
		}

		[HttpGet]
		[Route("GetConsultant/{id}")]
		public async Task<IActionResult> GetConsultant(Guid id)
		{
			var response = await this.consultantService.GetConsultant(id);

			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}

			var consultant = response.Models.FirstOrDefault();
			return Ok(consultant);
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery] GlobalFilter filter)
		{
			var consultantsList = await this.consultantService.GetAllAsync();

			var consultants = ConsultantFilter.Filter(consultantsList, filter);

			var result = PaginatedList<ConsultantResponse>.Create(
				consultants.ToList(), filter.PageNumber ?? 1, filter.PageSize ?? 20
				);

			return Ok(result);
		}

		[HttpPost]
		[Route("CreateConsultant")]
		public async Task<IActionResult> CreateConsultant([FromBody] ConsultantRequest request)
		{
			var response = await this.consultantService.CreateConsultantAsync(request);
			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}

			var createdConsultant = response.Models.FirstOrDefault();
			return Ok(createdConsultant);
		}

		[HttpPut]
		[Route("UpdateConsultant/{id}")]
		public async Task<IActionResult> UpdateConsultant(Guid id, ConsultantRequest request)
		{
			var response = await this.consultantService.UpdateConsultantAsync(id, request);
			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}

			var updatetConsultant = response.Models.FirstOrDefault();
			return Ok(updatetConsultant);
		}

		[HttpDelete]
		[Route("DeleteConsultant/{id}")]
		public async Task<IActionResult> DeleteConsultant(Guid id)
		{
			var response = await this.consultantService.DeleteConsultantAsync(id);
			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}
			return Ok();
		}
	}
}
