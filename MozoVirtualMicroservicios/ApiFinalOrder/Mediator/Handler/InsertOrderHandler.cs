using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Input;
using ApiFinalOrder.Integrations;
using ApiFinalOrder.Mediator.Commands;
using ApiFinalOrder.Services.IServices;
using LibreriaWinniePod;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinalOrder.Mediator.Handler
{
    public class InsertOrderHandler : IRequestHandler<InsertOrderCommand, OrderDTOWithProduct>
    {
        private readonly IOrderServices _orderService;
        private readonly IHttpManager _httpManager;

        public InsertOrderHandler(IOrderServices orderService, IHttpManager httpManager)
        {
            _orderService = orderService;
            _httpManager = httpManager;
        }

        public async Task<OrderDTOWithProduct> Handle(InsertOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Insert of the order in the DB
                var orderDTO = await _orderService.Insert(
                    MappeadorGenerico.MapEntityWithInnerList<OrderToInsertDTO,
                    ProductToInsertInput,
                    ProductIdQtyToInsert>
                    (request.order));
                //Change the Mesa to occupped
                AbrirMesaDTO requestBody = new() { OrderId = orderDTO.Id.Value, MesaId = orderDTO.TableNumber };
                string uriApiMesaAbrir = "https://localhost:7090/gateway/Mesa/Abrir";
                var (statusCode, output) = await _httpManager.PostAsync<AbrirMesaDTO, bool>(uriApiMesaAbrir, requestBody, request.order.Token);
                if (!output)
                {
                    return MappeadorGenerico.CreateEntityDTOWithError<OrderDTOWithProduct>(
                    new List<string>() { $"No se encontro la mesa disponible para la orden{orderDTO.Id}" });
                }
                //Discount the products from the stock
                string uriApiInventarioUpdateStock = "https://localhost:7090/gateway/inventario/UpdatedOrder";
                var (statusCodeInventario, outputInventario) = await _httpManager.PutAsync<
                    List<ProductToInsertInput>, bool>(
                            uriApiInventarioUpdateStock,
                            request.order.Products,
                            request.order.Token);
                if (!outputInventario)
                {
                    return MappeadorGenerico.CreateEntityDTOWithError<OrderDTOWithProduct>(
                        new List<string>() { $"No se pudo descontar  los productos del inventario" });
                }
                return orderDTO;
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);

                if (ex.InnerException.Message!=null)
                {
                    errorList.Add(ex.InnerException.Message);
                }
                
                return MappeadorGenerico.CreateEntityDTOWithError<OrderDTOWithProduct>(errorList);
            }
        }
    }
}
