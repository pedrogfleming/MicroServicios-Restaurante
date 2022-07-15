using ApiInventario.Commands;
using ApiInventario.Services.IService;
using MediatR;

namespace ApiInventario.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductService _productService;
        public DeleteProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            return _productService.Delete(command.Id);
        }
    }
}
