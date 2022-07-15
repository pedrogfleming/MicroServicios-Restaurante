using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Mediator.Queries;
using ApiFinalOrder.Services.IServices;
using MediatR;

namespace ApiFinalOrder.Mediator.Handler
{
    public class GetOrderByWaiterIdHandler : IRequestHandler<GetOrderByWaiterIdQuery, List<OrderDTO>>
    {
        private readonly IOrderServices _orderServices;
        public GetOrderByWaiterIdHandler(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public  Task<List<OrderDTO>> Handle(GetOrderByWaiterIdQuery request, CancellationToken cancellationToken)
        {
            return _orderServices.GetByWaiterId(request.WaiterId);
        }
    }
}
