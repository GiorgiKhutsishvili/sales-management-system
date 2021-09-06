using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SMS.Core.Models.Sales.Response;
using SMS.Core.Models.Sales.Request;
using SMS.Core.Models;
using SMS.Core.Entities;

namespace SMS.Core.Interfaces.Services
{
	public interface ISaleService
	{
		Task<IEnumerable<SaleResponse>> GetAllAsync();

		Task<GenericResponse<SaleResponse>> GetSaleProductAsync(Guid saleId);

		Task<Response> CreateSaleAsync(SaleRequest request);

		Task<GenericResponse<SaleResponse>> UpdateSaleAsync(Guid id, SaleRequest request);

		Task<Response> DeleteSaleAsync(Guid id);
	}
}
