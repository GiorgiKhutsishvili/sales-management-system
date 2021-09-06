using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Persistence.Uow;

namespace SMS.Persistence
{
	public static class PersistenceIoCConfig
	{
		public static void Setup(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<SMSDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			AddUow(services, configuration);
			AddDbInitializer(services);
		}

		private static void AddUow(IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IUnitOfWork>(ctx => new UnitOfWork(ctx.GetRequiredService<SMSDbContext>()));
			
		}

		private static void AddDbInitializer(IServiceCollection services)
		{
			services.AddTransient<DbInitializer>();
		}
	}
}