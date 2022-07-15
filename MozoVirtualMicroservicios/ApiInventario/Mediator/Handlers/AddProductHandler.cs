using ApiInventario.Commands;
using ApiInventario.DTOs;
using ApiInventario.Services.IService;
using MediatR;

namespace ApiInventario.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, ProductDTO>
    {
        private readonly IProductService _productService;

        public AddProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<ProductDTO> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            return _productService.Insert(command.ProductDto);
        }
    }
}
