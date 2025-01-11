

using MediatR;

namespace ShippingOrderService.Application.Commands.AddShippingOrderItem;

public record AddShippingOrderItemCommand(int ShippingOrderId, string Description, decimal Weight) : IRequest<bool>;
