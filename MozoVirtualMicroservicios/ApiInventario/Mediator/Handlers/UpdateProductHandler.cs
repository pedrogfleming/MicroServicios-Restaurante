using ApiInventario.Commands;
using ApiInventario.DTOs;
using ApiInventario.Services.IService;
using MediatR;

namespace ApiInventario.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDTO>
    {
        private readonly IProductService _productService;

        public UpdateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<ProductDTO> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            return _productService.Update(command.ProductDto);
        }

    }
}
