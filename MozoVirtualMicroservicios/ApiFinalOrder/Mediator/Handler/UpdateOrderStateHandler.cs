using ApiFinalOrder.DTO_s;
using ApiFinalOrder.ExternalServices.IExternalServices;
using ApiFinalOrder.Mediator.Commands;
using ApiFinalOrder.Services.IServices;
using ApiMesa.Domain.Enums;
using LibreriaWinniePod;
using MediatR;

namespace ApiFinalOrder.Mediator.Handler
{
    public class UpdateOrderStateHandler : IRequestHandler<UpdateOrderStateCommand, OrderDTO>
    {
        private readonly IOrderServices _orderService;
        private readonly IExternalServicesInventario _externalService;

        public UpdateOrderStateHandler(IOrderServices orderService, IExternalServicesInventario externalService)
        {
            _orderService = orderService;
            _externalService = externalService;
        }

        public Task<OrderDTO> Handle(UpdateOrderStateCommand request, CancellationToken cancellationToken)
        {
            var newOrderStatus = request.orderStateInput.Status;
            var oldOrder = _orderService.GetById(request.orderStateInput.Id.Value).Result;
            var oldOrderStatus = oldOrder.Status;
            var errorList = new List<string>();
            /*Cambios de estados esperados:
             * Pendiente se establece al crear la orden
             * Pendiente->Preparacion
             * Pendiente->Cancelada
             * Preparacion->Lista
             * Lista->Cerrada se establece con abonar la orden
             */
            switch (newOrderStatus)
            {
                case (int)EEstadosOrden.EnPreparacion:
                    if (oldOrderStatus == (int)EEstadosOrden.Pendiente)
                    {
                        oldOrder.Status = (int)EEstadosOrden.EnPreparacion;
                        return _orderService.Update(MappeadorGenerico.Map<OrderDTO>(oldOrder));
                    }
                    else
                    {
                        errorList.Add("Cambio de estado no valido");
                        return Task.FromResult(MappeadorGenerico.CreateEntityDTOWithError<OrderDTO>(errorList));
                    }
                case (int)EEstadosOrden.Cancelada:
                    if (oldOrderStatus == (int)EEstadosOrden.Pendiente &&
                        (oldOrder.CreationDate-DateTime.Now).TotalMinutes<=5)
                    {
                        oldOrder.Status = (int)EEstadosOrden.Cancelada;
                        var prodsToChange = new List<ProductIdQtyToInsert>();
                        foreach (var item in oldOrder.Products)
                        {
                            prodsToChange.Add(new ProductIdQtyToInsert()
                            { Id = item.Id.Value, Quantity = item.Quantity });
                        }
                        if (_externalService.UpdateStock(prodsToChange,request.token).Result)
                        {
                            return _orderService.Update(MappeadorGenerico.Map<OrderDTO>(oldOrder));
                        }
                        else
                        {
                            errorList.Add("Error al actualizar el stock");
                            return Task.FromResult(MappeadorGenerico.CreateEntityDTOWithError<OrderDTO>(errorList));
                        }
                    }
                    else
                    {
                        errorList.Add("Cambio de estado no valido");
                        return Task.FromResult(MappeadorGenerico.CreateEntityDTOWithError<OrderDTO>(errorList));
                    }
                case (int)EEstadosOrden.Lista:
                    if (oldOrderStatus == (int)EEstadosOrden.EnPreparacion)
                    {
                        oldOrder.Status = (int)EEstadosOrden.Lista;
                        return _orderService.Update(MappeadorGenerico.Map<OrderDTO>(oldOrder));
                    }
                    else
                    {
                        errorList.Add("Cambio de estado no valido");
                        return Task.FromResult(MappeadorGenerico.CreateEntityDTOWithError<OrderDTO>(errorList));
                    }
                default:
                    errorList.Add("Cambio de estado no valido");
                return Task.FromResult(MappeadorGenerico.CreateEntityDTOWithError<OrderDTO>(errorList));

            }
        }
    }
}
