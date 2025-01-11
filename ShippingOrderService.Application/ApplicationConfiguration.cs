
using Microsoft.Extensions.DependencyInjection;
using ShippingOrderService.Application.Commands.AddShippingOrderItem;
using ShippingOrderService.Application.Commands.ChangeShippingOrderState;
using ShippingOrderService.Application.Commands.CreateShippingOrder;
using ShippingOrderService.Application.Commands.RemoveShippingOrderItem;
namespace ShippingOrderService.Application
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddShippingOrderItemCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ChangeShippingOrderStateCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateShippingOrderCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RemoveShippingOrderItemCommand).Assembly));
        }
    }
}
