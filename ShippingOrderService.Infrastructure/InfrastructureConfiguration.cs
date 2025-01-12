using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShippingOrderService.Domain.Interfaces;
using ShippingOrderService.Infrastructure.Persistence;
using ShippingOrderService.Infrastructure.Repositories;

namespace ShippingOrderService.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShippingOrderDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
 
            services.AddScoped<IShippingOrderRepository, ShippingOrderRepository>();
        }
    }
}
