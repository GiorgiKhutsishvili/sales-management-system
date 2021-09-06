using SMS.Core.Entities;
using SMS.Business.Services;
using SMS.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Persistence;

namespace SMS.Business
{
	public static class BusinessIoCConfig
	{
		public static void Setup(IServiceCollection services, IConfiguration configuration)
		{
			PersistenceIoCConfig.Setup(services, configuration);
			AddServices(services);
		}

		private static void AddServices(IServiceCollection services)
		{
			services.AddScoped<IConsultantService, ConsultantService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ISaleService, SaleService>();
			services.AddScoped<IReportService, ReportService>();
		}
	}
}
