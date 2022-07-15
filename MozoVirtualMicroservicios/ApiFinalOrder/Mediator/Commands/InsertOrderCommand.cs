using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Input;
using MediatR;

namespace ApiFinalOrder.Mediator.Commands
{
    public class InsertOrderCommand : IRequest<OrderDTOWithProduct>
    {
        public OrderToInsertInput order { get; set; }

        public InsertOrderCommand(OrderToInsertInput order)
        {
            this.order = order;
        }
    }
}
