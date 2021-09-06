using SMS.Core.Entities;
using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Reports.Responses
{
	//გაყიდვები კონსულტანტების მიხედვით
	public class SalesByConsultantsResponse : Consultant
	{
		public SalesByConsultantsResponse(int index, SaleEntity sale)
		{
			this.Index = index + 1;
			this.Id = sale.Id;
			this.SaleUniqueNumber = sale.UniqueNumber;
			this.SaleDate = sale.DateCreated.ToString("dd/MM/yyyy");
			this.DateCreated = sale.DateCreated;
			this.ConsultantUniqueNumber = sale?.Consultant.UniqueNumber;
			this.ConsultantFullName = $"{sale?.Consultant.FirstName} {sale?.Consultant.LastName}";
			this.ConsultantPersonalId = sale?.Consultant.PersonalId;
			this.SoldProductQuantity = sale.SalesProducts.Sum(x => x.ProductCount);
			this.SoldProductPriceSum = sale.SalesProducts.Sum(x => x.Product.Price * x.ProductCount);
		}

		public string SaleUniqueNumber { get; set; }

		public string SaleDate { get; set; }

		public DateTime DateCreated { get; set; }

		public int SoldProductQuantity { get; set; }

		public decimal SoldProductPriceSum { get; set; }
	}
}
