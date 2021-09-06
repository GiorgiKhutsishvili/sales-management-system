using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Common.Models.ReportFilter
{
	public class ReportFilter
	{
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }
		public string SoldProductCode { get; set; }
		public int? SoldProductMinQuantity { get; set; }
	}
}
