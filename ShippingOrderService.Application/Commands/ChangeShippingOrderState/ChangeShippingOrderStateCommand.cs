
using MediatR;
using ShippingOrderService.Domain.Enums;

namespace ShippingOrderService.Application.Commands.ChangeShippingOrderState;

public record ChangeShippingOrderStateCommand(int ShippingOrderId, ShippingOrderState NewState) : IRequest<bool>;
