# Use official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution file
COPY ShippingOrderService.sln ./

# Copy all project files
COPY ShippingOrderService.API/ ShippingOrderService.API/
COPY ShippingOrderService.Application/ ShippingOrderService.Application/
COPY ShippingOrderService.Domain/ ShippingOrderService.Domain/
COPY ShippingOrderService.Infrastructure/ ShippingOrderService.Infrastructure/
COPY ShippingOrderService.Tests/ ShippingOrderService.Tests/

# Restore dependencies
RUN dotnet restore ShippingOrderService.sln

# Build the application
RUN dotnet publish ShippingOrderService.API/ShippingOrderService.API.csproj -c Release -o out

# Use runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port
EXPOSE 5000
EXPOSE 5001

# Run the application
ENTRYPOINT ["dotnet", "ShippingOrderService.API.dll"]
