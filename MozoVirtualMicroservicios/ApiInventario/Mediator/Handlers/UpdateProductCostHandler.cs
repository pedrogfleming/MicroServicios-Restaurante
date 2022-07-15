using ApiInventario.Commands;
using ApiInventario.Services.IService;
using MediatR;

namespace ApiInventario.Handlers
{
    public class UpdateProductCostHandler : IRequestHandler<UpdateProductCostCommand, bool>
    {
        private readonly IProductService _productService;
        public UpdateProductCostHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<bool> Handle(UpdateProductCostCommand command, CancellationToken cancellationToken)
        {
            return _productService.UpdateCost(command.Category, command.Cost);
        }
    }
}
