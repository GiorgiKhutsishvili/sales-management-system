using SMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Products.Response
{
	public class ProductResponse
	{
		public ProductResponse(ProductEntity product, int? index)
		{
			this.Index = index.HasValue ? index + 1 : 0;
			this.Id = product.Id;
			this.Code = product.Code;
			this.Name = product.Name;
			this.Price = product.Price;
			this.DateCreated = product.DateCreated;
		}

		public int? Index { get; set; }

		public Guid Id { get; set; }

		public string Code { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public DateTime DateCreated { get; set; }
	}
}
