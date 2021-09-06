using Microsoft.EntityFrameworkCore;
using SMS.Core.Entities;
using SMS.Persistence.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Persistence
{
	public class DbInitializer
	{
		private readonly SMSDbContext context;
		private readonly IUnitOfWork unitOfWork;

		public DbInitializer(SMSDbContext context, IUnitOfWork unitOfWork)
		{
			this.context = context;
			this.unitOfWork = unitOfWork;
		}

		public async Task Seed()
		{
			this.context.Database.EnsureCreated();

			var consultants = await this.GetConsultants();
			var products = await this.GetProducts();

			if (!consultants.Any() && !products.Any())
			{
				#region CreateConsultants
				var uniqueNumber = (uint)Guid.NewGuid().GetHashCode();
				consultants = new List<ConsultantEntity>
				{
					new ConsultantEntity
					{
						Id = Guid.NewGuid(),
						UniqueNumber = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						FirstName = "Steven A.",
						LastName = "Smith",
						PersonalId = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Gender = "Male",
						BirthDate = new DateTime(1991, 08, 01),
						DateCreated = DateTime.Now
					},
					new ConsultantEntity
					{
						Id = Guid.NewGuid(),
						UniqueNumber = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						FirstName = "Kathleen J.",
						LastName = "Streetman",
						PersonalId = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Gender = "Female",
						BirthDate = new DateTime(1978, 2, 14),
						DateCreated = DateTime.Now
					},
					new ConsultantEntity
					{
						Id = Guid.NewGuid(),
						UniqueNumber = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						FirstName = "Rolf B.",
						LastName = "Tidwell",
						PersonalId = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Gender = "Male",
						BirthDate = new DateTime(1988, 02, 15),
						DateCreated = DateTime.Now
					},
					new ConsultantEntity
					{
						Id = Guid.NewGuid(),
						UniqueNumber = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						FirstName = "James K.",
						LastName = "Pieper",
						PersonalId = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Gender = "Male",
						BirthDate = new DateTime(1992, 02, 01),
						DateCreated = DateTime.Now
					},
					new ConsultantEntity
					{
						Id = Guid.NewGuid(),
						UniqueNumber = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						FirstName = "Josefina S.",
						LastName = "Brodeur",
						PersonalId = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Gender = "Female",
						BirthDate = new DateTime(1956, 12, 19),
						DateCreated = DateTime.Now
					},
					new ConsultantEntity
					{
						Id = Guid.NewGuid(),
						UniqueNumber = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						FirstName = "Lauren G.",
						LastName = "Romero",
						PersonalId = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Gender = "Male",
						BirthDate = new DateTime(1991, 06, 29),
						DateCreated = DateTime.Now
					}
				};
				await this.unitOfWork.Context().AddRangeAsync(consultants);
				#endregion

				#region CreateProducts
				products = new List<ProductEntity>
				{
					new ProductEntity
					{
						Id = Guid.NewGuid(),
						Code = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Name = "Lenovo V15 ADA",
						Price = 1599,
					},
					new ProductEntity
					{
						Id = Guid.NewGuid(),
						Code = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Name = "Apple iPhone 11 64GB",
						Price = 2399,
					},
					new ProductEntity
					{
						Id = Guid.NewGuid(),
						Code = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Name = "Apple iPhone 12 mini",
						Price = 2799,
					},
					new ProductEntity
					{
						Id = Guid.NewGuid(),
						Code = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Name = "Samsung Galaxy Tab S7",
						Price = 2199,
					},
					new ProductEntity
					{
						Id = Guid.NewGuid(),
						Code = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Name = "Samsung TV UE43T5370AUXRU",
						Price = 1669,
					},
					new ProductEntity
					{
						Id = Guid.NewGuid(),
						Code = Convert.ToString((uint)Guid.NewGuid().GetHashCode()),
						Name = "Samsung Galaxy Watch",
						Price = 1019,
					}
				};
				await this.unitOfWork.Context().AddRangeAsync(products);
				#endregion

				await this.unitOfWork.Context().SaveChangesAsync();
			}
		}

		#region Private Methods
		private async Task<IEnumerable<ConsultantEntity>> GetConsultants()
		{
			var consultants = await this.unitOfWork.Context()
				.Set<ConsultantEntity>()
				.AsNoTracking()
				.ToListAsync();

			return consultants;
		}

		private async Task<IEnumerable<ProductEntity>> GetProducts()
		{
			var products = await this.unitOfWork.Context()
				.Set<ProductEntity>()
				.AsNoTracking()
				.ToListAsync();

			return products;
		}
		#endregion
	}
}