
using MediatR;

namespace ShippingOrderService.Application.Commands.RemoveShippingOrderItem;

public record RemoveShippingOrderItemCommand(int ShippingOrderId, int ItemId) : IRequest<bool>;
