using ApiInventario.DTOs;
using MediatR;

namespace ApiInventario.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }
}
