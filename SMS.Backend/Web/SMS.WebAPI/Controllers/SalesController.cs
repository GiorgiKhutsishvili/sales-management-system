using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.Core.Interfaces.Services;
using SMS.Core.Models.Sales.Request;
using SMS.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMS.Core.Models.Sales.Response;
using SMS.Common.Models.Filter;
using SMS.Business.Filters;

namespace SMS.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SalesController : ControllerBase
	{
		private readonly ISaleService saleService;

		public SalesController(ISaleService saleService)
		{
			this.saleService = saleService;
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery] GlobalFilter filter)
		{
			var salesList = await this.saleService.GetAllAsync();

			var sales = SaleFilter.Filter(salesList, filter);

			var result = PaginatedList<SaleResponse>.Create(
				sales.ToList(), filter.PageNumber ?? 1, filter.PageSize ?? 20
			);

			return Ok(result);
		}

		[HttpGet]
		[Route("GetSaleProduct/{id}")]
		public async Task<IActionResult> GetSaleProduct(Guid id)
		{
			var response = await this.saleService.GetSaleProductAsync(id);

			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}

			var sale = response.Models.FirstOrDefault();
			return Ok(sale);
		}

		[HttpPost]
		[Route("CreateSale")]
		public async Task<IActionResult> CreateSale([FromBody] SaleRequest request)
		{
			var response = await this.saleService.CreateSaleAsync(request);
			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}
			return Ok();
		}

		[HttpPut]
		[Route("UpdateSale/{id}")]
		public async Task<IActionResult> UpdateSale(Guid id, [FromBody] SaleRequest request)
		{
			if (request.ConsultantId == Guid.Empty)
			{
				return BadRequest();
			}

			var response = await this.saleService.UpdateSaleAsync(id, request);
			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}

			var updatedSale = response.Models.FirstOrDefault();
			return Ok(updatedSale);
		}

		[HttpDelete]
		[Route("DeleteSale/{id}")]
		public async Task<IActionResult> DeleteSale(Guid id)
		{
			var response = await this.saleService.DeleteSaleAsync(id);
			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}
			return Ok();
		}
	}
}
