using ApiFinalOrder.Domain.Model;
using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Infrastructure;
using ApiFinalOrder.Services.IServices;
using ApiMesa.Domain.Enums;
using LibreriaWinniePod;

namespace ApiFinalOrder.Services
{
    public class OrderServices : IOrderServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IProductOrderService _productOrderService;

        public OrderServices(IProductOrderService productOrderService, IProductService productService, IUnitOfWork  unitOfWork)
        {
            _productOrderService = productOrderService;
            _productService = productService;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderDTO>> GetAll()
        {
            return MappeadorGenerico.MapEntities<OrderDTO>(_unitOfWork.Orders.GetAll());
        }

        public async Task<OrderDTOWithProduct> GetById(int id)
        {
            var productOrder = await _unitOfWork.ProductOrders.GetAsync(x => x.IdOrder == id);
            List<ProductDTO> productos = new List<ProductDTO>();
            foreach (var item in productOrder)
            {
                productos.Add(await _productService.GetById(item.IdProduct));
            }            
            var orderDTOWithProduct = MappeadorGenerico.Map<OrderDTOWithProduct>(_unitOfWork.Orders.GetById(id));
            orderDTOWithProduct.Products = productos;
            return orderDTOWithProduct;
        }
        
        public async Task<OrderDTOWithProduct> Insert(OrderToInsertDTO dto)
        {
            var orderNew = new Order()
            {
                WaiterId = dto.WaiterId,
                CreationDate = DateTime.Now,
                Customers = dto.Customers,
                ServiceFee = dto.ServiceFee,
                TableNumber = dto.TableNumber,
                TotalCost = dto.TotalCost,
                Status = dto.Status
            };
            var o = _unitOfWork.Orders.Insert(orderNew);
            if(_unitOfWork.Save() == 0) {
                return MappeadorGenerico.CreateEntityDTOWithError<OrderDTOWithProduct>(new() {
                    $"No se pudo crear la orden de la mesa{dto.TableNumber} de las {dto.CreationDate.ToShortTimeString()}"
                });
            }
            var order = new OrderDTOWithProduct()
            {
                Id = o.Id,
                WaiterId = o.WaiterId,
                CreationDate = o.CreationDate,
                Customers = o.Customers,
                ServiceFee = o.ServiceFee,
                TableNumber = o.TableNumber,
                TotalCost = o.TotalCost,
                Status = o.Status,
                Products = await InsertProductOrder(dto.Products, o.Id)
            };
            return _unitOfWork.Save() > 0 ? order :
                MappeadorGenerico.CreateEntityDTOWithError<OrderDTOWithProduct>(new() {
                    $"No se pudo agregar los productos a la orden {o.Id} de la mesa {dto.TableNumber} de las {dto.CreationDate.ToShortTimeString()}"
                });
        }
        public int SaveFeo()
        {
            return _unitOfWork.Save();
        }
        
        public async Task<List<ProductDTO>> InsertProductOrder(List<ProductIdQtyToInsert> products, int orderId)
        {
            var listProductos = new List<ProductDTO>();
            foreach (var item in products)
            {
                await _productOrderService.AddProduct(item, orderId);
                var p = await _productService.GetById(item.Id);
                listProductos.Add(p);
            }
            return listProductos;
        }
        public async Task<OrderDTO> Update(OrderDTO dto)
        {
            var o = _unitOfWork.Orders.Update(MappeadorGenerico.Map<Order>(dto));
            return MappeadorGenerico.Map<OrderDTO>(o);
        }

        public async Task<List<OrderDTOWithProduct>> GetAllOrdersWithProduct()
        {
            List<OrderDTOWithProduct> orderDTOWithProductsList = new ();
            var orders = _unitOfWork.Orders.GetAll();
            var productos = _unitOfWork.Products.GetAll();

            foreach (var order in orders)
            {
                var productOrders = (_unitOfWork.ProductOrders.GetAsync(x => x.IdOrder == order.Id)).Result;
                foreach (var productOrder in productOrders)
                {
                    var listaProduct = MappeadorGenerico.MapEntities<ProductDTO>(
                                                productos.Where(x => x.Id == productOrder.IdProduct));
                    var o = MappeadorGenerico.Map<OrderDTOWithProduct>(order);
                    o.Products = listaProduct;
                    orderDTOWithProductsList.Add(o);
                }
            }
            return orderDTOWithProductsList;


        }

        public async Task<bool> DeleteProductOrder(List<ProductIdQtyToInsert> products, int orderId)
        {
            if (CanChangeProducts(orderId).Result)
            {
                return false;
            }
                var result = await _productOrderService.RemoveProduct(products,orderId);
                if (!result)
                {
                    return false;
                }                
            return true;
        }

        private async Task<bool> CanChangeProducts(int idOrder)
        {
            var orderDto = await GetById(idOrder);
            return !(orderDto.Status == (int)EEstadosOrden.Pendiente);
        }


        public async Task<List<OrderDTO>> GetByWaiterId(int waiterId)
        {
            var result = _unitOfWork.Orders.GetAsync(filter: x => x.WaiterId == waiterId).Result;
            List<OrderDTO> rMap = new List<OrderDTO>();
            foreach (var item in result)
            {
                rMap.Add(MappeadorGenerico.MapEntityWithInnerList<OrderDTO, Product, ProductDTO>(item));
            }
            return rMap;
        }

        public async Task<decimal> GetTotalFeeByWaiter(int waiterId)
        {
            return GetByWaiterId(waiterId).Result.Sum(x => x.ServiceFee);
        }

        public Task<OrderDTO> Pay(int orderId, decimal payment)
        {
            var order = _unitOfWork.Orders.GetById(orderId);
            if (order.TotalCost == payment)
            {
                order.Status = (int)EEstadosOrden.Cerrada;
                _unitOfWork.Orders.Update(order);
                return Task.FromResult(MappeadorGenerico.Map<OrderDTO>(order));
            }
            else
            {
                var errorList = new List<string>
                {
                    "The payment is not enough to cover the order's cost"
                };
                return Task.FromResult(MappeadorGenerico.CreateEntityDTOWithError<OrderDTO>(errorList));
            }
        }

    }
}
