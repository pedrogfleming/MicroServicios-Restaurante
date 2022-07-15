using ApiInventario.Commands;
using ApiInventario.DTOs;
using ApiInventario.Services.IService;
using MediatR;

namespace ApiInventario.Handlers
{
    public class UpdateStockByUpdatedOrdeHandler : IRequestHandler<UpdateStockByUpdatedOrderCommand, bool>
    {
        private readonly IProductService _productService;

        public UpdateStockByUpdatedOrdeHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<bool> Handle(UpdateStockByUpdatedOrderCommand command, CancellationToken cancellationToken)
        {
            return _productService.UpdateStockByUpdatedOrder(command.products);
        }
    }
}
