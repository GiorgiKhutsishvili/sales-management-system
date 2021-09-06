using SMS.Core.Entities;
using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Reports.Responses
{
	//კონსულტანტები ხშირად გაყიდვადი პროდუქტების მიხედვით:
	public class ConsultantsByFrequentlySoldProductsResponse : Consultant
	{
		public ConsultantsByFrequentlySoldProductsResponse(int index, SaleEntity sale)
		{
			this.Index = index + 1;
			this.Id = sale.Id;
			this.ConsultantUniqueNumber = sale?.Consultant.UniqueNumber;
			this.ConsultantFullName = $"{sale?.Consultant.FirstName} {sale?.Consultant.LastName}";
			this.ConsultantPersonalId = sale?.Consultant.PersonalId;
			this.ConsultantBirthDate = sale.Consultant.BirthDate.ToString("dd/MM/yyyy");
			this.SoldProductCode = sale.SalesProducts.FirstOrDefault().Product?.Code;
			this.SoldProductMinQuantity = sale.SalesProducts.FirstOrDefault().ProductCount;
			this.DateCreated = sale.DateCreated;
		}

		public string SoldProductCode { get; set; }

		public int SoldProductMinQuantity { get; set; }

		public DateTime DateCreated { get; set; }
	}
}
