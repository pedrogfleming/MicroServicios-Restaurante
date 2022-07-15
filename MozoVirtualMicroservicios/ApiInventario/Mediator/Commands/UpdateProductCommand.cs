using ApiInventario.DTOs;
using MediatR;

namespace ApiInventario.Commands
{
    public class UpdateProductCommand : IRequest<ProductDTO>
    {
        public ProductDTO ProductDto { get; set; }
        public UpdateProductCommand(ProductDTO productDto)
        {
            ProductDto = productDto;
        }
    }
}
