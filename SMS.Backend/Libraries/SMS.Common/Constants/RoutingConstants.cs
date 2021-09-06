using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Common.Constants
{
	public static class RoutingConstants
	{
		//Reports
		public const string SalesByConsultants = "GetSalesByConsultants";
		public const string SalesByProductPrices = "GetSalesByProductPrices";
		public const string ConsultantsByFrequentlySoldProducts = "GetConsultantsByFrequentlySoldProducts";
		public const string ConsultantsBySumSales = "GetConsultantsBySumSales";
		public const string ConsultantsByMostSoldProducts = "GetConsultantsByMostSoldProducts"; 
	}
}
