using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Mediator.Queries;
using ApiFinalOrder.Services.IServices;
using MediatR;

namespace ApiFinalOrder.Mediator.Handler
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDTOWithProduct>
    {
        private readonly IOrderServices _orderService;

        public GetOrderByIdHandler(IOrderServices orderService)
        {
            _orderService = orderService;
        }

        public Task<OrderDTOWithProduct> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return _orderService.GetById(request.id);
        }
    }
}
