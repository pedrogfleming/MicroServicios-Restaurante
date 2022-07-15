using ApiInventario.Input;
using MediatR;

namespace ApiInventario.Commands
{
    public class UpdateStockByUpdatedOrderCommand : IRequest<bool>
    {
        public List<ProductToInsertInput> products { get; }

        public UpdateStockByUpdatedOrderCommand(List<ProductToInsertInput> products)
        {
            this.products = products;
        }
    }
}