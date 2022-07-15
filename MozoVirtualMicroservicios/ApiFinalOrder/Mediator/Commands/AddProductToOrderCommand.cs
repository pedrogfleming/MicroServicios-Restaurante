using ApiFinalOrder.Input;
using MediatR;

namespace ApiFinalOrder.Mediator.Commands
{
    public class AddProductToOrderCommand : IRequest<bool>
    {
        public string token { get; set; }
        public int orderId { get; set; }
        public List<ProductToInsertInput> products { get; set; }

        public AddProductToOrderCommand(List<ProductToInsertInput> products, int orderId,string token)
        {
            this.products = products;
            this.orderId = orderId;
            this.token = token;
        }
    }
}
