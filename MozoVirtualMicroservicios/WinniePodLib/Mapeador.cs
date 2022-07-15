using ApiFinalOrder.Domain.Model;
using ApiFinalOrder.DTO_s;
using FluentAssertions;

namespace WinniePodLibTests
{
    [Trait("Category", "Mappers")]
    public class Mapeador
    {
        [Fact]
        public void Order_A_DTO_OK()
        {
            var order = new Mock<Order>().Object;
            var dto = new Mock<OrderDTO>().Object;
            var result = MappeadorGenerico.Map<Order>(dto);            
            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public void DTO_A_Order_OK()
        {
            var dto = new Mock<OrderDTO>().Object;
            var order = new Mock<Order>().Object;
            var result  = MappeadorGenerico.Map<OrderDTO>(dto);
            result.Should().BeEquivalentTo(dto);
        }
        [Fact]
        public void MappearOrderDTO_ThrowsException()
        {
            var dto = new Mock<OrderDTO>().Object;
            // ??
            Assert.ThrowsAny<Exception>(() => MappeadorGenerico.Map<OrderDTO>(null));
        }
        [Fact]
        public void MapEntityWithInnerList_ThrowsException()
        {
            var dto = new Mock<OrderDTOWithProduct>().Object;                        
            //I pass in the wrong order the type parameters (it must be MapEntityWithInnerList<Order,ProductDTO,Product>(dto);
            Assert.ThrowsAny<Exception>(() => MappeadorGenerico.MapEntityWithInnerList<OrderDTOWithProduct, ProductDTO, Product>(dto));
        }
        [Fact]
        public void MapEntitiesOrders_ThrowsException()
        {
            var orderList = new Mock<List<OrderDTO>>().Object;
            orderList.Add(new Mock<OrderDTO>().Object);
            orderList.Add(new Mock<OrderDTO>().Object);
            orderList.Add(new Mock<OrderDTO>().Object);
            
            //Le pasa lista de órdenes para mapear a lista de productos.
            Assert.ThrowsAny<Exception>(() => MappeadorGenerico.MapEntities<List<Product>>(orderList));

            //I pass in the wrong order the type parameters (it must be MapEntityWithInnerList<Order,ProductDTO,Product>(dto);
            //Assert.ThrowsAny<Exception>(() => MappeadorGenerico.MapEntities<List<Product>>(orders));
        }

        [Fact]
        public void MapEntitiesProducts_Ok()
        {
            var products = new Mock<List<Product>>().Object;
            products.Add(new Mock<Product>().Object);
            products.Add(new Mock<Product>().Object);

            var result = MappeadorGenerico.MapEntities<ProductDTO>(products);
            Assert.NotEmpty(result);
        }
        [Fact]
        public void MapEntities_WithDiferrentsProperties_ThrowException()
        {
            var order = new Mock<OrderDTO>().Object;

            Assert.ThrowsAny<Exception>(() => MappeadorGenerico.Map<ProductDTO>(order));
        }
    }
}