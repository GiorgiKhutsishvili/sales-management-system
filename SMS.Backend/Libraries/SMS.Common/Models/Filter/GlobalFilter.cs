using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Common.Models.Filter
{
	public class GlobalFilter
	{
		public string SortOrder { get; set; }
		public string CurrentFilter { get; set; }
		public string SearchString { get; set; }
		public int? PageNumber { get; set; }
		public int? PageSize { get; set; }
	}
}
