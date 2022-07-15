using ApiCocinero.Services.IService;
using ApiOrder.Queries;
using MediatR;

namespace ApiOrder.Handlers
{
    public class GetTotalFeeHandler : IRequestHandler<GetTotalFeeQuery, decimal>
    {
        private readonly IOrderService _orderService;

        public GetTotalFeeHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Task<decimal> Handle(GetTotalFeeQuery request, CancellationToken cancellationToken)
        {
            return _orderService.GetTotalFeeByWaiter(request.WaiterId);
        }
    }
}
