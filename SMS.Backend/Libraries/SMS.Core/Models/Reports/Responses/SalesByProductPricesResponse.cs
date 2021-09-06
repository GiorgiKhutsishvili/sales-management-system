using SMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Reports.Responses
{
	//გაყიდვები პროდუქციის ფასების მიხედვით:
	public class SalesByProductPricesResponse : Consultant
	{
		public SalesByProductPricesResponse(int index, SaleEntity sale)
		{
			this.Index = index + 1;
			this.Id = sale.Id;
			this.SaleUniqueNumber = sale.UniqueNumber;
			this.SaleDate = sale.DateCreated.ToString("dd/MM/yyyy");
			this.DateCreated = sale.DateCreated;
			this.ConsultantUniqueNumber = sale?.Consultant.UniqueNumber;
			this.ConsultantFullName = $"{sale?.Consultant.FirstName} {sale?.Consultant.LastName}";
			this.ConsultantPersonalId = sale?.Consultant.PersonalId;
			this.ProductPrice = sale.SalesProducts.FirstOrDefault().Product.Price;
			this.ProductCount = sale.SalesProducts.FirstOrDefault().ProductCount;
		}

		public string SaleUniqueNumber { get; set; }

		public string SaleDate { get; set; }

		public DateTime DateCreated { get; set; }

		public decimal ProductPrice { get; set; }

		public int ProductCount { get; set; }
	}
}
