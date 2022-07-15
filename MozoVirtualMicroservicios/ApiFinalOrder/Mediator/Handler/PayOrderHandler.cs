using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Mediator.Commands;
using ApiFinalOrder.Services.IServices;

namespace ApiFinalOrder.Mediator.Handler
{
    public class PayOrderHandler
    {
        private readonly IOrderServices _orderServices;

        public PayOrderHandler(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public Task<OrderDTO> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            return _orderServices.Pay(request.Id, request.Amount);
        }
    }

}
