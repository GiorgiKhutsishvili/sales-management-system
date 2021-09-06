using SMS.Common.Models.Filter;
using SMS.Core.Models.Products.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Business.Filters
{
	public class ProductFilter
	{
		public static IEnumerable<ProductResponse> Filter(IEnumerable<ProductResponse> products, GlobalFilter filter)
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
				products = products.Where(s => s.Code.Contains(filter.SearchString)
											|| s.Name.Contains(filter.SearchString)
											|| s.Price == Convert.ToDecimal(filter.SearchString));
			}

			switch (filter.SortOrder)
			{
				case "code_desc":
					products = products.OrderByDescending(s => s.Code);
					break;
				case "name_desc":
					products = products.OrderByDescending(s => s.Code);
					break;
				case "pcice_desc":
					products = products.OrderByDescending(s => s.Price);
					break;
				case "date_created_desc":
					products = products.OrderByDescending(s => s.DateCreated);
					break;
				default:
					products = products.OrderByDescending(s => s.Index);
					break;
			}
			return products;
		}
	}
}
