using ApiFinalOrder.DTO_s;
using MediatR;

namespace ApiFinalOrder.Mediator.Queries
{
    public class GetAllOrdersWithProductQuery : IRequest<List<OrderDTOWithProduct>>
    {
    }
}
