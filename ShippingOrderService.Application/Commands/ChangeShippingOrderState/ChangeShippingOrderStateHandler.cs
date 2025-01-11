using MediatR;
using ShippingOrderService.Domain.Interfaces;

namespace ShippingOrderService.Application.Commands.ChangeShippingOrderState
{
    public class ChangeShippingOrderStateHandler : IRequestHandler<ChangeShippingOrderStateCommand, bool>
    {
        private readonly IShippingOrderRepository _repository;

        public ChangeShippingOrderStateHandler(IShippingOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ChangeShippingOrderStateCommand request, CancellationToken cancellationToken)
        {
            var shippingOrder = await _repository.GetByIdAsync(request.ShippingOrderId);

            if (shippingOrder == null)
                throw new InvalidOperationException($"Shipping order with ID {request.ShippingOrderId} not found.");

            shippingOrder.ChangeState(request.NewState);

            var result = await _repository.SaveChangesAsync();

            return result;
        }
    }
}
