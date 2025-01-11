using MediatR;

namespace ShippingOrderService.Application.Commands.CreateShippingOrder;

public record CreateShippingOrderCommand(string TrackingNumber, DateTime ShippingDate) : IRequest<int>;
