
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
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



            // Add FluentValidation
            services.AddControllers();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            // Register Validators using Assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(ms => ms.Value.Errors.Any())
                        .Select(ms => new
                        {
                            Field = ms.Key,
                            Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                        });

                    return new BadRequestObjectResult(new { Message = "Validation failed", Errors = errors });
                };
            });

        }
    }
}
