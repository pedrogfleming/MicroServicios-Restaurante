using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Mediator.Queries;
using ApiFinalOrder.Services.IServices;
using MediatR;

namespace ApiFinalOrder.Mediator.Handler
{
    public class GetAllOrdersWithoutProductHandler : IRequestHandler<GetAllOrdersWithoutProductQuery, List<OrderDTO>>
    {

        private readonly IOrderServices _orderServices;

        public GetAllOrdersWithoutProductHandler(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public async Task<List<OrderDTO>> Handle(GetAllOrdersWithoutProductQuery request, CancellationToken cancellationToken)
        {
            return await _orderServices.GetAll();
        }
    }
}
