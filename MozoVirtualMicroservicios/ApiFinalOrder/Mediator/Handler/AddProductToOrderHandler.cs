using ApiFinalOrder.DTO_s;
using ApiFinalOrder.ExternalServices.IExternalServices;
using ApiFinalOrder.Mediator.Commands;
using ApiFinalOrder.Services.IServices;
using LibreriaWinniePod;
using MediatR;

namespace ApiFinalOrder.Mediator.Handler
{
    public class AddProductToOrderHandler : IRequestHandler<AddProductToOrderCommand, bool>
    {
        private readonly IOrderServices _orderService;
        private readonly IExternalServicesInventario _externalService;

        public AddProductToOrderHandler(IOrderServices orderService, IExternalServicesInventario externalService)
        {
            _orderService = orderService;
            _externalService = externalService;
        }

        public async Task<bool> Handle(AddProductToOrderCommand request, CancellationToken cancellationToken)
        {
            var addProductToOrder = _orderService.InsertProductOrder(MappeadorGenerico.MapEntities<ProductIdQtyToInsert>(request.products), request.orderId);
            _orderService.SaveFeo();
            var result = await _externalService.UpdateStock(MappeadorGenerico.MapEntities<ProductIdQtyToInsert>(request.products),request.token);
            return result;
        }
    }
}
