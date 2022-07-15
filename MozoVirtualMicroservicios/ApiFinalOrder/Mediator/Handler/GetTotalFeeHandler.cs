using ApiFinalOrder.Mediator.Queries;
using ApiFinalOrder.Services.IServices;
using MediatR;

namespace ApiFinalOrder.Mediator.Handler
{
    public class GetTotalFeeHandler : IRequestHandler<GetTotalFeeQuery, decimal>
    {
        private readonly IOrderServices _orderServices;

        public GetTotalFeeHandler(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public Task<decimal> Handle(GetTotalFeeQuery request, CancellationToken cancellationToken)
        {
            return _orderServices.GetTotalFeeByWaiter(request.WaiterId);
        }
    }
}
