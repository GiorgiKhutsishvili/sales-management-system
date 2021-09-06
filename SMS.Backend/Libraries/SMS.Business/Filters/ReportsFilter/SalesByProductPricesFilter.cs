﻿using SMS.Common.Models.Filter;
using SMS.Common.Models.ReportFilter;
using SMS.Core.Models.Reports.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Business.Filters.ReportsFilter
{
	public class SalesByProductPricesFilter
	{
		public static IEnumerable<SalesByProductPricesResponse> Filter(
			IEnumerable<SalesByProductPricesResponse> list,
			ReportFilter reportFilter, GlobalFilter globalFilter)
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
					list = list.Where(s => s.ConsultantFullName.Contains(globalFilter.SearchString)
													  || s.ConsultantPersonalId.Contains(globalFilter.SearchString)
													  || s.ConsultantUniqueNumber.Contains(globalFilter.SearchString)
													  || s.SaleUniqueNumber.Contains(globalFilter.SearchString));
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
						list = list.OrderByDescending(s => s.SaleUniqueNumber);
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
					list = list.Where(c => c.DateCreated <= reportFilter.EndDate.Value);
				}

				if (reportFilter.MinPrice.HasValue)
				{
					list = list.Where(c => c.ProductPrice >= reportFilter.MinPrice.Value);
				}

				if (reportFilter.MaxPrice.HasValue)
				{
					list = list.Where(c => c.ProductPrice <= reportFilter.MaxPrice.Value);
				}
			}

			return list;
		}
	}
}
