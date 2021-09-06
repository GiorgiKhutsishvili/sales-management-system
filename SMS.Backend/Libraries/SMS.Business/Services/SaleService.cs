using SMS.Core.Interfaces.Services;
using SMS.Core.Models;
using SMS.Core.Models.Sales.Request;
using SMS.Core.Models.Sales.Response;
using System;
using SMS.Persistence.Uow;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Core.Entities;
using SMS.Business.Resources;

namespace SMS.Business.Services
{
	public class SaleService : ISaleService
	{
		private readonly IUnitOfWork unitOfWork;

		public SaleService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<SaleResponse>> GetAllAsync()
		{
			var query = await this.BaseQuery();
			var sales = query.Select((index, entity) =>
				new SaleResponse(index, entity));
			return sales;
		}

		public async Task<GenericResponse<SaleResponse>> GetSaleProductAsync(Guid spId)
		{
			var response = new GenericResponse<SaleResponse>();

			var product = await GetSaleProduct(spId);
			if (product == null)
			{
				response.AddError(string.Format(SharedResource.Errors_SaleIsNotFound, spId));
				return response;
			}

			var saleResponse = new SaleResponse(product, null);
			response.Models.Add(saleResponse);
			return response;
		}

		public async Task<Response> CreateSaleAsync(SaleRequest request)
		{
			var response = new Response();

			var uniqueNumber = (uint)Guid.NewGuid().GetHashCode();
			var saleEntity = new SaleEntity
			{
				Id = Guid.NewGuid(),
				ConsultantId = request.ConsultantId,
				UniqueNumber = uniqueNumber.ToString(),
				DateCreated = DateTime.Now
			};
			await this.unitOfWork.Context().AddAsync(saleEntity);

			foreach (var product in request.Products)
			{
				if(product.ProductCount <= 0)
				{
					response.AddError(SharedResource.Errors_ProductQtyShoulNotBeLessThanZero);
					return response;
				}

				var saleProductEntity = new SaleProductEntity
				{
					SpId = Guid.NewGuid(),
					SaleId = saleEntity.Id,
					ProductId = product.ProductId,
					ProductCount = product.ProductCount
				};
				await this.unitOfWork.Context().AddAsync(saleProductEntity);
			}
			await this.unitOfWork.CommitAsync();
			return response;
		}

		public async Task<GenericResponse<SaleResponse>> UpdateSaleAsync(Guid id, SaleRequest request)
		{
			var response = new GenericResponse<SaleResponse>();
			var product = await GetSaleProduct(id);
			if (product == null)
			{
				response.AddError(SharedResource.Errors_SaleIsNotFound);
				return response;
			}

			var requestedProduct = request.Products.FirstOrDefault();
			if (requestedProduct.ProductCount <= 0)
			{
				response.AddError(SharedResource.Errors_ProductQtyShoulNotBeLessThanZero);
				return response;
			}

			product.Sale.ConsultantId = request.ConsultantId;

			if (product.ProductId != requestedProduct.ProductId)
			{
				var query = await this.BaseQuery();
				var productExists = query.Any(x => x.ProductId == requestedProduct.ProductId);
				if (productExists)
				{
					string consultantFullName = $"{product.Sale.Consultant.FirstName} {product.Sale.Consultant.LastName}";
					response.AddError(string.Format(SharedResource.Errors_SameProductIsAlreadyAddedToConsultant, consultantFullName));
					return response;
				}

				this.unitOfWork.Context().Remove(product);
				var newSale = new SaleProductEntity
				{
					SpId = Guid.NewGuid(),
					SaleId = product.SaleId,
					ProductId = requestedProduct.ProductId,
					ProductCount = requestedProduct.ProductCount
				};
				await this.unitOfWork.Context().AddAsync(newSale);
			}
			else
			{
				product.ProductCount = requestedProduct.ProductCount;
			}

			product.Sale.DateChanged = DateTime.Now;
			await this.unitOfWork.CommitAsync();
			return response;
		}

		public async Task<Response> DeleteSaleAsync(Guid id)
		{
			var response = new Response();

			var product = await GetSaleProduct(id);
			if (product == null)
			{
				response.AddError(SharedResource.Errors_SaleIsNotFound);
				return response;
			}

			product.Sale.DateDeleted = DateTime.Now;

			this.unitOfWork.Context().Remove(product);
			await this.unitOfWork.CommitAsync();

			return response;
		}

		#region Private Methods
		private async Task<SaleEntity> Get(Guid id)
		{
			var sale = await this.unitOfWork.Context()
				.Set<SaleEntity>()
				.Include(x => x.Consultant)
				.Include(s => s.SalesProducts
				.Where(n => n.Product.DateDeleted == null))
				.ThenInclude(x => x.Product)
				.Where(d => d.DateDeleted == null && d.Consultant.DateDeleted == null)
				.FirstOrDefaultAsync(c => c.Id == id);

			return sale;
		}

		private async Task<SaleProductEntity> GetSaleProduct(Guid spId)
		{
			var query = await BaseQuery();
			var product = query.FirstOrDefault(x => x.SpId == spId);
			return product;
		}

		private async Task<IEnumerable<SaleProductEntity>> BaseQuery()
		{
			var query = await this.unitOfWork.Context()
				.Set<SaleProductEntity>()
				.Include(s => s.Sale)
				.ThenInclude(c => c.Consultant)
				.Include(p => p.Product)
				.Where(d => d.Sale.DateDeleted == null
						 && d.Sale.Consultant.DateDeleted == null)
				.ToListAsync();

			return query;
		}
		#endregion
	}
}
