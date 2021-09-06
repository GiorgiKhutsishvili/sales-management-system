using SMS.Common.Models.Filter;
using SMS.Common.Models.ReportFilter;
using SMS.Core.Models.Reports.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Business.Filters.ReportsFilter
{
	public class ConsultantsByFrequentlySoldProductsFilter
	{
		public static IEnumerable<ConsultantsByFrequentlySoldProductsResponse> Filter(
			IEnumerable<ConsultantsByFrequentlySoldProductsResponse> list,
			ReportFilter reportFilter, GlobalFilter globalFilter
			)
		{
			if (globalFilter != null)
			{
				if (globalFilter.SearchString != null)
				{
					globalFilter.PageNumber = 1;
				}
				else
				{
					globalFilter.SearchString = globalFilter.CurrentFilter;
				}

				if (!string.IsNullOrEmpty(globalFilter.SearchString))
				{
					list = list.Where(c => c.ConsultantFullName.Contains(globalFilter.SearchString)
													  || c.ConsultantPersonalId.Contains(globalFilter.SearchString)
													  || c.ConsultantUniqueNumber.Contains(globalFilter.SearchString));
				}

				switch (globalFilter.SortOrder)
				{
					case "personalId_desc":
						list = list.OrderByDescending(s => s.ConsultantPersonalId);
						break;
					case "unique_number_desc":
						list = list.OrderByDescending(s => s.ConsultantUniqueNumber);
						break;
					case "sale_unique_number_desc":
						list = list.OrderByDescending(s => s.SoldProductCode);
						break;
					default:
						list = list.OrderByDescending(s => s.Index);
						break;
				}
			}

			if (reportFilter != null)
			{
				if (reportFilter.StartDate.HasValue)
				{
					list = list.Where(c => c.DateCreated >= reportFilter.StartDate.Value);
				}

				if (reportFilter.EndDate.HasValue)
				{
					reportFilter.EndDate = reportFilter.EndDate.Value.AddDays(1).AddTicks(-1);
					list = list.Where(x => x.DateCreated <= reportFilter.EndDate.Value);
				}

				if (!string.IsNullOrWhiteSpace(reportFilter.SoldProductCode))
				{
					list = list.Where(x => x.SoldProductCode.Contains(reportFilter.SoldProductCode));
				}

				if (reportFilter.SoldProductMinQuantity.HasValue)
				{
					list = list.Where(c => c.SoldProductMinQuantity >= reportFilter.SoldProductMinQuantity.Value);
				}
			}

			return list;
		}
	}
}
