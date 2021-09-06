using SMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Sales.Response
{
	public class SaleResponse
	{
		public SaleResponse(SaleProductEntity saleProduct, int? index)
		{
			this.Id = index.HasValue ? index + 1 : 0;
			this.SpId = saleProduct.SpId;
			this.SaleId = saleProduct.SaleId;
			this.ConsultantId = saleProduct.Sale.ConsultantId;
			this.UniqueNumber = saleProduct?.Sale?.UniqueNumber;
			this.SaleDate = saleProduct.Sale.DateCreated.ToString("dd/MM/yyyy");
			this.Consultant = $"{saleProduct.Sale?.Consultant?.FirstName} {saleProduct.Sale?.Consultant?.LastName}";
			this.ProductId = saleProduct.ProductId;
			this.SoldProduct = saleProduct?.Product?.Name;
			this.ProductCount = saleProduct.ProductCount;

		}
		public int? Id { get; set; }

		public Guid SpId { get; set; }

		public Guid SaleId { get; set; }

		public Guid ConsultantId { get; set; }

		public Guid ProductId { get; set; }

		public string UniqueNumber { get; set; }

		public string SaleDate { get; set; }

		public string Consultant { get; set; }

		public string SoldProduct { get; set; }

		public int ProductCount { get; set; }
	}
}
