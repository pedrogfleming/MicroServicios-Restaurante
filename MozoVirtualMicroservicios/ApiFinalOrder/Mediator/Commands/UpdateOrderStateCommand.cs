using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Input;
using MediatR;

namespace ApiFinalOrder.Mediator.Commands
{
    public class UpdateOrderStateCommand : IRequest<OrderDTO>
    {
        public OrderStateToUpdate orderStateInput;
        public string token { get; set; }

        public UpdateOrderStateCommand(OrderStateToUpdate orderStateInput, string token)
        {
            this.orderStateInput = orderStateInput;
            this.token = token;
        }
    }
}
