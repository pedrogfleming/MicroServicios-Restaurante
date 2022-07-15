using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Mediator.Queries;
using ApiFinalOrder.Services.IServices;
using MediatR;

namespace ApiFinalOrder.Mediator.Handler
{
    public class GetAllOrdersWithProductHandler : IRequestHandler<GetAllOrdersWithProductQuery, List<OrderDTOWithProduct>>
    {

        private readonly IOrderServices _orderServices;


        public GetAllOrdersWithProductHandler(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public async Task<List<OrderDTOWithProduct>> Handle(GetAllOrdersWithProductQuery request, CancellationToken cancellationToken)
        {
            return await _orderServices.GetAllOrdersWithProduct();
        }
    }
}
