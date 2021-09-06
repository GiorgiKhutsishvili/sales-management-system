using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SMS.Core.Models.Consultants.Response;
using SMS.Core.Models.Consultants.Request;
using SMS.Core.Models;
using SMS.Core.Entities;
using SMS.Core.Models.Products.Response;
using SMS.Core.Models.Products.Request;

namespace SMS.Core.Interfaces.Services
{
	public interface IProductService
	{
		Task<GenericResponse<ProductResponse>> GetProduct(Guid id);

		Task<IEnumerable<ProductResponse>> GetAllAsync();

		Task<GenericResponse<ProductResponse>> CreateProductAsync(ProductRequest request);

		Task<GenericResponse<ProductResponse>> UpdateProductAsync(Guid id, ProductRequest request);

		Task<Response> DeleteProductAsync(Guid id);
	}
}
