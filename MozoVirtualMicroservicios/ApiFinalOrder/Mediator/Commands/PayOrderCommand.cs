using ApiFinalOrder.DTO_s;
using MediatR;

namespace ApiFinalOrder.Mediator.Commands
{
    public class PayOrderCommand : IRequest<OrderDTO>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

    }
}
