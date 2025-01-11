using ShippingOrderService.Infrastructure;
using ShippingOrderService.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureConfiguration(builder.Configuration);
builder.Services.AddApplicationConfiguration();

builder.Services.AddControllers();


//NSwag
builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "Shipping Order Service";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
