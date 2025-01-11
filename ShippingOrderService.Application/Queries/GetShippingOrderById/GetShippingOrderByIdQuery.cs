
using MediatR;
using ShippingOrderService.Application.Responses;

namespace ShippingOrderService.Application.Queries.GetShippingOrderById;

public record GetShippingOrderByIdQuery(int ShippingOrderId) : IRequest<ShippingOrderResponse>;
