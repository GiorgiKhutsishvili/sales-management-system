using SMS.Core.Models.Reports.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Interfaces.Services
{
	public interface IReportService
	{
		Task<IEnumerable<SalesByConsultantsResponse>> GetSalesByConsultantsAsync();
		Task<IEnumerable<SalesByProductPricesResponse>> GetSalesByProductPricesAsync();
		Task<IEnumerable<ConsultantsByFrequentlySoldProductsResponse>> GetConsultantsByFrequentlySoldProductsAsync();
		Task<IEnumerable<ConsultantsBySumSalesResponse>> GetConsultantsBySumSalesAsync();
		Task<IEnumerable<ConsultantsByMostSoldProductsResponse>> GetConsultantsByMostSoldProductsAsync();
	}
}
