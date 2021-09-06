using SMS.Core.Entities;
using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Reports.Responses
{
	public class ConsultantsByMostSoldProductsResponse : Consultant
	{
		public string MostSoldProductCode { get; set; }

		public string MostSoldProductName { get; set; }

		public int MostSoldProductQuantity { get; set; }

		public string MostProfitableProductCode { get; set; }

		public string MostProfitableProductName { get; set; }

		public decimal MostProfitableProductSaleSumAmount { get; set; }

		public DateTime DateCreated { get; set; }
	}
}
