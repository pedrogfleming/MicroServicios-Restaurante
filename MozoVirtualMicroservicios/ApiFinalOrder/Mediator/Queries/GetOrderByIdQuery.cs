using ApiFinalOrder.DTO_s;
using MediatR;

namespace ApiFinalOrder.Mediator.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDTOWithProduct>
    {
        public int id { get; set; }

        public GetOrderByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
