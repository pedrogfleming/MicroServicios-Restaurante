using ApiFinalOrder.DTO_s;
using MediatR;

namespace ApiFinalOrder.Mediator.Queries
{
    public class GetOrderByWaiterIdQuery : IRequest<List<OrderDTO>>
    {
        public int WaiterId { get; set; }
        public GetOrderByWaiterIdQuery(int waiterId)
        {
            this.WaiterId = waiterId;
        }
    }
}
