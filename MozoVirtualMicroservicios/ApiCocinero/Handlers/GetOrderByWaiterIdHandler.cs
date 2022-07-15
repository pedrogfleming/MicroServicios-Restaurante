using ApiCocinero.DTOs;
using ApiCocinero.Services.IService;
using ApiOrder.Queries;
using ApiOrder.Services.IService;
using MediatR;

namespace ApiOrder.Handlers
{
    public class GetOrderByWaiterIdHandler : IRequestHandler<GetOrderByWaiterIdQuery, List<OrderDTO>>
    {
        private readonly IOrderService _orderService;
        public GetOrderByWaiterIdHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Task<List<OrderDTO>> Handle(GetOrderByWaiterIdQuery request, CancellationToken cancellationToken)
        {
            return _orderService.GetByWaiterId(request._waiterId);            
        }
    }
}
