using SMS.Common.Models.Filter;
using SMS.Core.Models.Consultants.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Business.Filters
{
	public class ConsultantFilter
	{
		public static IEnumerable<ConsultantResponse> Filter(IEnumerable<ConsultantResponse> consultants, GlobalFilter filter)
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
				consultants = consultants.Where(s => s.FirstName.Contains(filter.SearchString)
												  || s.LastName.Contains(filter.SearchString)
												  || s.PersonalId.Contains(filter.SearchString)
												  || s.UniqueNumber.Contains(filter.SearchString)
												  || s.Gender.Contains(filter.SearchString));
			}

			switch (filter.SortOrder)
			{
				case "name_desc":
					consultants = consultants.OrderByDescending(s => s.LastName);
					break;
				case "date_created_desc":
					consultants = consultants.OrderByDescending(s => s.DateCreated);
					break;
				case "birth_date_desc":
					consultants = consultants.OrderByDescending(s => s.BirthDate);
					break;
				default:
					consultants = consultants.OrderByDescending(s => s.Index);
					break;
			}
			return consultants;
		}
	}
}
