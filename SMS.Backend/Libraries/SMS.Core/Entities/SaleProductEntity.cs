using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Core.Entities
{
	public class SaleProductEntity
	{
		[Key]
		public Guid SpId { get; set; }

		[Key]
		[Column(Order = 1)]
		public Guid SaleId { get; set; }

		[ForeignKey(nameof(SaleId))]
		public SaleEntity Sale { get; set; }

		[Key]
		[Column(Order = 2)]
		public Guid ProductId { get; set; }

		[ForeignKey(nameof(ProductId))]
		public ProductEntity Product { get; set; }

		public int ProductCount { get; set; }

		public void Configure(EntityTypeBuilder<SaleProductEntity> builder)
		{
			builder
				.ToTable("SalesProducts")
				.HasKey(sp => new { sp.SaleId, sp.ProductId });

			builder
				.HasOne(sp => sp.Sale)
				.WithMany(s => s.SalesProducts)
				.HasForeignKey(sp => sp.SaleId);

			builder
				.HasOne(sp => sp.Product)
				.WithMany(s => s.SalesProducts)
				.HasForeignKey(sp => sp.ProductId);

			builder
			.HasOne(c => c.Sale)
			.WithMany()
			.OnDelete(DeleteBehavior.NoAction);

			builder
			.HasOne(c => c.Product)
			.WithMany()
			.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
