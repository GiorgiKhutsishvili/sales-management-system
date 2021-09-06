using Microsoft.EntityFrameworkCore;
using SMS.Core.Entities;
using SMS.Core.Interfaces.Services;
using SMS.Core.Models.Reports.Responses;
using SMS.Persistence.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Business.Services
{
	public class ReportService : IReportService
	{
		private readonly IUnitOfWork unitOfWork;
		public ReportService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<SalesByConsultantsResponse>> GetSalesByConsultantsAsync()
		{
			var query = await this.BaseQuery();
			var salesByConsultatns = query.Select((entity, index) =>
				new SalesByConsultantsResponse(index, entity));
			return salesByConsultatns;
		}

		public async Task<IEnumerable<SalesByProductPricesResponse>> GetSalesByProductPricesAsync()
		{
			var query = await this.BaseQuery();
			var sales = query.Select((entity, index) =>
				new SalesByProductPricesResponse(index, entity));
			return sales;
		}

		public async Task<IEnumerable<ConsultantsByFrequentlySoldProductsResponse>> GetConsultantsByFrequentlySoldProductsAsync()
		{
			var query = await this.BaseQuery();
			var consultantSales = query.Select((entity, index) =>
				new ConsultantsByFrequentlySoldProductsResponse(index, entity));
			return consultantSales;
		}

		public async Task<IEnumerable<ConsultantsBySumSalesResponse>> GetConsultantsBySumSalesAsync()
		{
			var consultants = await this.GetConsultants();

			var query = await this.BaseQuery();
			var consultantsBySumSales = query.Select((entity, index) =>
				new ConsultantsBySumSalesResponse(index, entity, consultants));
			return consultantsBySumSales;
		}

		public async Task<IEnumerable<ConsultantsByMostSoldProductsResponse>> GetConsultantsByMostSoldProductsAsync()
		{
			var query = await GetProductSales();
			var consLp = query.ToLookup(x => x.Consultant);
			var list = new List<ConsultantsByMostSoldProductsResponse>();

			int index = 0;
			foreach (var item in consLp)
			{
				index++;

				var sales = consLp[item.Key];
				var soldProducts = sales.SelectMany(x => x.SalesProducts).ToList();
				var soldProductsLp = soldProducts.ToLookup(x => x.Product.Code);
				var maxCount = soldProductsLp.Max(x => x.Sum(n => n.ProductCount));
				var maxPrice = soldProductsLp.Max(x => x.Sum(n => n.Product.Price * n.ProductCount));
				var mostSoldProduct = soldProductsLp.Aggregate((x, y) => x.Sum(n => n.ProductCount) > y.Sum(e => e.ProductCount) ? x : y).First();
				var mostProficientProduct = soldProductsLp.Aggregate((x, y) => x.Sum(n => n.ProductCount * n.Product.Price) > y.Sum(e => e.ProductCount) ? x : y).First();

				var consultant = item.Key;

				var response = new ConsultantsByMostSoldProductsResponse
				{
					Index = index,
					Id = Guid.NewGuid(),
					ConsultantUniqueNumber = consultant.UniqueNumber,
					ConsultantFullName = $"{consultant.FirstName} {consultant.LastName}",
					ConsultantPersonalId = consultant.PersonalId,
					ConsultantBirthDate = consultant.BirthDate.ToString("dd/MM/yyyy"),

					MostSoldProductCode = mostSoldProduct.Product.Code,
					MostSoldProductName = mostSoldProduct.Product.Name,
					MostSoldProductQuantity = maxCount,

					MostProfitableProductCode = mostProficientProduct.Product.Code,
					MostProfitableProductName = mostProficientProduct.Product.Name,
					MostProfitableProductSaleSumAmount = maxPrice,
					DateCreated = mostSoldProduct.Sale.DateCreated
				};

				list.Add(response);
			}
			return list;
		}

		#region Private Methods
		private async Task<IEnumerable<SaleEntity>> BaseQuery()
		{
			var sales = await this.unitOfWork.Context()
				.Set<SaleEntity>()
				.AsNoTracking()
				.Include(x => x.Consultant)
				.Include(s => s.SalesProducts)
				.ThenInclude(x => x.Product)
				.Where(d => d.DateDeleted == null &&
							d.Consultant.DateDeleted == null)
				.ToListAsync();

			return sales;
		}

		private async Task<IEnumerable<ConsultantEntity>> GetConsultants()
		{
			var consultants = await this.unitOfWork.Context()
				.Set<ConsultantEntity>()
				.AsNoTracking()
				.Include(s => s.Sales)
				.ThenInclude(sp => sp.SalesProducts)
				.ThenInclude(x => x.Product)
				.Where(x => x.RecommendatorId != null &&
							x.Sales.Count > 0 &&
							x.DateDeleted == null)
				.ToListAsync();
			return consultants;
		}

		private async Task<IEnumerable<SaleEntity>> GetProductSales()
		{
			var query = await this.unitOfWork.Context()
				.Set<SaleEntity>()
				.AsNoTracking()
				.Include(sp=> sp.SalesProducts)
				.ThenInclude(pp => pp.Product)
				.Include(s => s.Consultant)
				.Where(d => d.DateDeleted == null)
				.ToListAsync();

			return query;
		}
		#endregion
	}
}