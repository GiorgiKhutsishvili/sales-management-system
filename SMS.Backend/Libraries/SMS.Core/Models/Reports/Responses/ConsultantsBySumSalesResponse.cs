using SMS.Core.Entities;
using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Reports.Responses
{
	//კონსულტანტები ჯამური გაყიდვების მიხედვით:
	public class ConsultantsBySumSalesResponse : Consultant
	{
		public ConsultantsBySumSalesResponse(int index, SaleEntity sale,
			IEnumerable<ConsultantEntity> consultants)
		{
			consultants = consultants.Where(x => x.RecommendatorId == sale.Consultant.Id);
			var sales = consultants.SelectMany(x => x.Sales);
			var salesProducts = sales.SelectMany(x => x.SalesProducts);
			var consultantHierarchySaleSum = salesProducts.Sum(x => x.Product.Price * x.ProductCount);

			this.Index = index + 1;
			this.Id = sale.Id;
			this.ConsultantUniqueNumber = sale?.Consultant.UniqueNumber;
			this.ConsultantFullName = $"{sale?.Consultant.FirstName} {sale?.Consultant.LastName}";
			this.ConsultantPersonalId = sale?.Consultant.PersonalId;
			this.ConsultantBirthDate = sale.Consultant.BirthDate.ToString("dd/MM/yyyy");

			this.ConsultantSaleSum = sale.SalesProducts.Sum(x => x.Product.Price * x.ProductCount);
			this.ConsultantHierarchySaleSum = consultantHierarchySaleSum + this.ConsultantSaleSum;
			this.DateCreated = sale.DateCreated;
		}
		public decimal ConsultantSaleSum { get; set; }

		public decimal ConsultantHierarchySaleSum { get; set; } //ინფორმაციის სორტირება უნდა მოხდეს ბოლო სვეტის მიხედვით კლებადობით.

		public DateTime DateCreated { get; set; }
	}
}
