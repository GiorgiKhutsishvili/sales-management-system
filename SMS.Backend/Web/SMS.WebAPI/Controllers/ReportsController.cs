using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.Business.Filters.ReportsFilter;
using SMS.Common.Constants;
using SMS.Common.Models.Filter;
using SMS.Common.Models.ReportFilter;
using SMS.Common.Utilities;
using SMS.Core.Interfaces.Services;
using SMS.Core.Models.Reports.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportsController : ControllerBase
	{
		private readonly IReportService reportService;

		public ReportsController(IReportService reportService)
		{
			this.reportService = reportService;
		}

		[HttpGet]
		[Route(RoutingConstants.SalesByConsultants)]
		public async Task<IActionResult> GetSalesByConsultants(
			[FromQuery] GlobalFilter globalFilter,
			[FromQuery(Name = "StartDate")] DateTime? startDate,
			[FromQuery(Name = "EndDate")] DateTime? endDate)
		{
			ReportFilter reportFilter = null;

			if (startDate.HasValue || endDate.HasValue)
			{
				reportFilter = new ReportFilter
				{
					StartDate = startDate,
					EndDate = endDate
				};
			}

			var list = await this.reportService.GetSalesByConsultantsAsync();

			var salesByConsultants = SalesByConsultantsFilter.Filter(list, reportFilter, globalFilter);

			var result = PaginatedList<SalesByConsultantsResponse>.Create(
				salesByConsultants.ToList(), globalFilter?.PageNumber ?? 1, globalFilter?.PageSize ?? 20);

			return Ok(result);
		}

		[HttpGet]
		[Route(RoutingConstants.SalesByProductPrices)]
		public async Task<IActionResult> GetSalesByProductPricesAsync(
			[FromQuery] GlobalFilter globalFilter,
			[FromQuery(Name = "StartDate")] DateTime? startDate,
			[FromQuery(Name = "EndDate")] DateTime? endDate,
			[FromQuery(Name = "MinPrice")] decimal? minPrice,
			[FromQuery(Name = "MaxPrice")] decimal? maxPrice)
		{
			ReportFilter reportFilter = null;

			if (startDate.HasValue ||
				endDate.HasValue ||
				minPrice.HasValue ||
				maxPrice.HasValue)
			{
				reportFilter = new ReportFilter
				{
					StartDate = startDate,
					EndDate = endDate,
					MinPrice = minPrice,
					MaxPrice = maxPrice,
				};
			}

			var list = await this.reportService.GetSalesByProductPricesAsync();

			var salesByProductPrices = SalesByProductPricesFilter.Filter(list, reportFilter, globalFilter);

			var result = PaginatedList<SalesByProductPricesResponse>.Create(
				salesByProductPrices.ToList(), globalFilter?.PageNumber ?? 1, globalFilter?.PageSize ?? 20);

			return Ok(result);
		}


		[HttpGet]
		[Route(RoutingConstants.ConsultantsByFrequentlySoldProducts)]
		public async Task<IActionResult> GetConsultantsByFrequentlySoldProducts(
			[FromQuery] GlobalFilter globalFilter,
			[FromQuery(Name = "StartDate")] DateTime? startDate,
			[FromQuery(Name = "EndDate")] DateTime? endDate,
			[FromQuery(Name = "SoldProductCode")] string soldProductCode,
			[FromQuery(Name = "SoldProductMinQuantity")] int? soldProductMinQuantity
			)
		{
			ReportFilter reportFilter = null;

			if (startDate.HasValue ||
				endDate.HasValue ||
				!string.IsNullOrWhiteSpace(soldProductCode) ||
				soldProductMinQuantity.HasValue)
			{
				reportFilter = new ReportFilter
				{
					StartDate = startDate,
					EndDate = endDate,
					SoldProductCode = soldProductCode,
					SoldProductMinQuantity = soldProductMinQuantity,
				};
			}

			var list = await this.reportService.GetConsultantsByFrequentlySoldProductsAsync();

			var consultantsByFrequentlySoldProducts = ConsultantsByFrequentlySoldProductsFilter.Filter(list, reportFilter, globalFilter);

			var result = PaginatedList<ConsultantsByFrequentlySoldProductsResponse>.Create(
				consultantsByFrequentlySoldProducts.ToList(), globalFilter?.PageNumber ?? 1, globalFilter?.PageSize ?? 20);

			return Ok(result);
		}


		[HttpGet]
		[Route(RoutingConstants.ConsultantsBySumSales)]
		public async Task<IActionResult> GetConsultantsBySumSales(
			[FromQuery] GlobalFilter globalFilter,
			[FromQuery(Name = "StartDate")] DateTime? startDate,
			[FromQuery(Name = "EndDate")] DateTime? endDate)
		{
			ReportFilter reportFilter = null;

			if (startDate.HasValue || endDate.HasValue)
			{
				reportFilter = new ReportFilter
				{
					StartDate = startDate,
					EndDate = endDate
				};
			}

			var list = await this.reportService.GetConsultantsBySumSalesAsync();

			var consultantsBySumSales = ConsultantsBySumSalesFilter.Filter(list, reportFilter, globalFilter);

			var result = PaginatedList<ConsultantsBySumSalesResponse>.Create(
				consultantsBySumSales.ToList(), globalFilter?.PageNumber ?? 1, globalFilter?.PageSize ?? 20);

			return Ok(result);
		}

		[HttpGet]
		[Route(RoutingConstants.ConsultantsByMostSoldProducts)]
		public async Task<IActionResult> GetConsultantsByMostSoldProducts(
			[FromQuery] GlobalFilter globalFilter,
			[FromQuery(Name = "StartDate")] DateTime? startDate,
			[FromQuery(Name = "EndDate")] DateTime? endDate)
		{
			ReportFilter reportFilter = null;

			if (startDate.HasValue || endDate.HasValue)
			{
				reportFilter = new ReportFilter
				{
					StartDate = startDate,
					EndDate = endDate
				};
			}

			var list = await this.reportService.GetConsultantsByMostSoldProductsAsync();

			var consultantsByMostSoldProducts = ConsultantsByMostSoldProductsFilter.Filter(list, reportFilter, globalFilter);

			var result = PaginatedList<ConsultantsByMostSoldProductsResponse>.Create(
				consultantsByMostSoldProducts.ToList(), globalFilter?.PageNumber ?? 1, globalFilter?.PageSize ?? 20);

			return Ok(result);
		}
	}
}
