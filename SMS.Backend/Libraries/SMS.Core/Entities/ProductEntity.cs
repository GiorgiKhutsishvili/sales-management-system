using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Core.Entities
{
	public class ProductEntity : ITrackable
	{
		[Key]
		public Guid Id { get; set; }

		public string Code { get; set; }

		public string Name { get; set; }

		[Column(TypeName = "decimal(16, 4)")]
		public decimal Price { get; set; }

		public virtual ICollection<SaleProductEntity> SalesProducts { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime? DateChanged { get; set; }

		public DateTime? DateDeleted { get; set; }
	}
}
