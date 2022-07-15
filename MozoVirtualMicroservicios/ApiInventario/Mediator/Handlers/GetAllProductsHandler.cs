using ApiInventario.DTOs;
using ApiInventario.Queries;
using ApiInventario.Services.IService;
using MediatR;

namespace ApiInventario.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IProductService _productService;
        public GetAllProductsHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            return _productService.GetAll();
        }
    }
}