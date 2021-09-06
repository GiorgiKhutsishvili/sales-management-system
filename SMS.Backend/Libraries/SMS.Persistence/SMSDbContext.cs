using Microsoft.EntityFrameworkCore;
using SMS.Core.Entities;

namespace SMS.Persistence
{
	public class SMSDbContext : DbContext
	{
		public SMSDbContext(DbContextOptions<SMSDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<SaleProductEntity>().HasKey(table => new
			{
				table.SaleId,
				table.ProductId
			});

			builder.Entity<ProductEntity>()
				.HasKey(b => new { b.Id });
		}

		public DbSet<ConsultantEntity> Consultants { get; set; }

		public DbSet<ProductEntity> Products { get; set; }

		public DbSet<SaleEntity> Sales { get; set; }

		public DbSet<SaleProductEntity> SalesProducts { get; set; }
	}
}
