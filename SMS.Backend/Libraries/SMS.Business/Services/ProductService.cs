using SMS.Core.Interfaces.Services;
using SMS.Core.Models;
using SMS.Core.Models.Products.Request;
using SMS.Core.Models.Products.Response;
using SMS.Persistence.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Core.Entities;
using SMS.Business.Resources;

namespace SMS.Business.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork unitOfWork;
		public ProductService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task<GenericResponse<ProductResponse>> GetProduct(Guid id)
		{
			var response = new GenericResponse<ProductResponse>();

			var product = await Get(id);
			if (product == null)
			{
				response.AddError(string.Format(SharedResource.Errors_ProductIsNotFound, id));
				return response;
			}

			var productResponse = new ProductResponse(product, null);
			response.Models.Add(productResponse);
			return response;
		}

		public async Task<IEnumerable<ProductResponse>> GetAllAsync()
		{
			var query = await this.BaseQuery();

			var products = query.Select((index, entity) =>
				new ProductResponse(index, entity));

			return products;
		}
		public async Task<GenericResponse<ProductResponse>> CreateProductAsync(ProductRequest request)
		{
			var genericResponse = new GenericResponse<ProductResponse>();
			var code = (uint)Guid.NewGuid().GetHashCode();
			var productEntity = new ProductEntity
			{
				Id = Guid.NewGuid(),
				Code = code.ToString(),
				Name = request.Name,
				Price = request.Price,
				DateCreated = DateTime.Now
			};

			await this.unitOfWork.Context().AddAsync(productEntity);
			await this.unitOfWork.CommitAsync();

			var response = new ProductResponse(productEntity, null);
			genericResponse.Models.Add(response);
			return genericResponse;
		}

		public async Task<GenericResponse<ProductResponse>> UpdateProductAsync(Guid id, ProductRequest request)
		{
			var response = new GenericResponse<ProductResponse>();

			var product = await Get(id);
			if (product == null)
			{
				response.AddError("პროდუქტი არ არსებობს");
				return response;
			}

			product.Name = request.Name;
			product.Price = request.Price;

			await this.unitOfWork.CommitAsync();

			var productResponse = new ProductResponse(product, null);
			response.Models.Add(productResponse);
			return response;
		}

		public async Task<Response> DeleteProductAsync(Guid id)
		{
			var response = new Response();

			var product = await Get(id);
			if (product == null)
			{
				response.AddError("პროდუქტი არ არსებობს");
				return response;
			}

			product.DateDeleted = DateTime.Now;
			await this.unitOfWork.CommitAsync();

			return response;
		}

		#region Private Methods
		private async Task<ProductEntity> Get(Guid id)
		{
			var consultant = await this.unitOfWork.Context()
				.Set<ProductEntity>()
				.FirstOrDefaultAsync(c => c.Id == id);

			return consultant;
		}

		private async Task<IEnumerable<ProductEntity>> BaseQuery()
		{
			var query = await this.unitOfWork.Context()
				.Set<ProductEntity>()
				.AsNoTracking()
				.Where(d => d.DateDeleted == null)
				.ToListAsync();

			return query;
		}
		#endregion
	}
}