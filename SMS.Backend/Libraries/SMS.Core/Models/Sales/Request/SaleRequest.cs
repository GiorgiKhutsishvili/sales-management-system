using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Sales.Request
{
	public class SaleRequest
	{
		public Guid ConsultantId { get; set; }
		public List<Product> Products { get; set; }
	}

	public class Product
	{
		public Guid ProductId { get; set; }
		public int ProductCount { get; set; }
	}
}
