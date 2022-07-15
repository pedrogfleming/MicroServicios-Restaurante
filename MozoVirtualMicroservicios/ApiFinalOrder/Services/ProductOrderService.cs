using ApiFinalOrder.Domain.Model;
using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Infrastructure;
using ApiFinalOrder.Services.IServices;
using LibreriaWinniePod;

namespace ApiFinalOrder.Services
{
    public class ProductOrderService : IProductOrderService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddProduct(ProductIdQtyToInsert dto, int orderId)
        {
            var newPO = new ProductOrder()
            {
                IdOrder = orderId,
                IdProduct = dto.Id,
                Quantity = dto.Quantity
            };
            var p = _unitOfWork.ProductOrders.Insert(newPO);
            return p != null ? true : false;
        }

        public async Task<bool> RemoveProduct(List<ProductIdQtyToInsert> dto,int orderId)
        {
            var orderProduct = _unitOfWork.ProductOrders.GetAsync(filter: x => x.IdOrder == orderId).Result;

            foreach (var item in dto)
            {
                var toRemove = orderProduct.FirstOrDefault(x => x.IdProduct == item.Id);

                if (toRemove.Quantity == item.Quantity)
                {
                    var result = _unitOfWork.ProductOrders.Delete(toRemove.Id);
                    if (result == null)
                    {
                        return false;
                    }
                }
                else
                {
                    toRemove.Quantity -= item.Quantity;
                    var result = _unitOfWork.ProductOrders.Update(MappeadorGenerico.Map<ProductOrder>(orderProduct));
                    if (result == null)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
    }
}
