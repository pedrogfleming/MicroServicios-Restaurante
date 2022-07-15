using ApiInventario.DTOs;
using ApiInventario.Input;
using MediatR;

namespace ApiInventario.Commands
{
    public class AddProductCommand : IRequest<ProductDTO>
    {
        public ProductToAddInput ProductDto { get; set; }
        public AddProductCommand(ProductToAddInput productDto)
        {
            ProductDto = productDto;
        }
    }
}
