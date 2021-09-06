using SMS.Common.Models.Filter;
using SMS.Core.Models.Sales.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Business.Filters
{
	public class SaleFilter
	{
		public static IEnumerable<SaleResponse> Filter(IEnumerable<SaleResponse> sales, GlobalFilter filter)
		{
			if (filter.SearchString != null)
			{
				filter.PageNumber = 1;
			}
			else
			{
				filter.SearchString = filter.CurrentFilter;
			}

			if (!string.IsNullOrEmpty(filter.SearchString))
			{
				sales = sales.Where(s => s.UniqueNumber.Contains(filter.SearchString)
												  || s.Consultant.Contains(filter.SearchString)
												  || s.SoldProduct.Contains(filter.SearchString));
			}

			switch (filter.SortOrder)
			{
				case "unique_number_desc":
					sales = sales.OrderByDescending(s => s.UniqueNumber);
					break;
				case "consultant_desc":
					sales = sales.OrderByDescending(s => s.Consultant);
					break;
				case "sold_product_desc":
					sales = sales.OrderByDescending(s => s.SoldProduct);
					break;
				case "date_created_desc":
					sales = sales.OrderByDescending(s => s.SaleDate);
					break;
				default:
					sales = sales.OrderByDescending(s => s.Id);
					break;
			}
			return sales;
		}
	}
}
