using ApiFinalOrder.DTO_s;
using ApiFinalOrder.Services.IServices;
using FluentAssertions;

namespace OrdersAPITests
{
    [Trait("Category", "Services")]
    public class Services
    {
        public Services()
        {

        }
        [Fact]
        public void GetAllOrders_Not_Empty()
        {
            var orderService = new Mock<IOrderServices>();

            IEnumerable<OrderDTO> orders = new List<OrderDTO>
                    {
                        new Mock<OrderDTO>().Object,
                        new Mock<OrderDTO>().Object
                    };

            orderService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(orders.ToList()));

            var result = orderService.Object.GetAll();

            Assert.NotEmpty(result.Result);
        }

        [Fact]
        public void GetAllOrders_Empty()
        {
            var orderService = new Mock<IOrderServices>();

            IEnumerable<OrderDTO> orders = new List<OrderDTO>();

            orderService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(orders.ToList()));

            var result = orderService.Object.GetAll();

            Assert.Empty(result.Result);
        }

        [Fact]
        public void Update_Equal()
        {
            var orderService = new Mock<IOrderServices>();
            var orderDTO = new Mock<OrderDTO>().Object;
            var emptyDTO = new OrderDTO();
            orderService.Setup(x => x.Update(orderDTO)).Returns(Task.FromResult(orderDTO));
            var result = orderService.Object.Update(orderDTO);
            Assert.Equal(orderDTO, result.Result);
        }

        [Fact]
        public void Update_Not_Equal()
        {
            var orderService = new Mock<IOrderServices>();
            var orderDTO = new Mock<OrderDTO>().Object;
            orderService.Setup(x => x.Update(orderDTO)).Returns(Task.FromResult(new Mock<OrderDTO>().Object));
            var result = orderService.Object.Update(orderDTO);
            Assert.NotEqual(orderDTO, result.Result);
        }

        [Fact]
        public void Insert_Success()
        {
            var service = new Mock<IOrderServices>();
            var orderToInsertDTO = new Mock<OrderToInsertDTO>().Object;
            var orderDTOWithProduct = new Mock<OrderDTOWithProduct>().Object;

            service.Setup(x => x.Insert(orderToInsertDTO))
                .Returns(Task.FromResult(orderDTOWithProduct));

            var result = service.Object.Insert(orderToInsertDTO);
            Assert.Equal(result.Result, orderDTOWithProduct);
        }

        [Fact]
        public void Insert_Fail()
        {
            var service = new Mock<IOrderServices>();
            var orderToInsertDTO = new Mock<OrderToInsertDTO>().Object;
            var orderDTOWithProduct = new Mock<OrderDTOWithProduct>().Object;

            service.Setup(x =>
                x.Insert(orderToInsertDTO))
                .Returns(Task.FromResult(new Mock<OrderDTOWithProduct>().Object));

            var result = service.Object.Insert(orderToInsertDTO);
            Assert.NotEqual(result.Result, orderDTOWithProduct);
        }
        [Fact]
        public void GetById_Ok()
        {
            var service = new Mock<IOrderServices>();
            var orderDTOWithProduct = new Mock<OrderDTOWithProduct>().Object;
            service.Setup(x => x.GetById(1)).Returns(Task.FromResult(new Mock<OrderDTOWithProduct>().Object));
            var result = service.Object.GetById(1);
            result.Result.Should().BeEquivalentTo(orderDTOWithProduct);
        }
        [Fact]
        public void GetById_Fail()
        {
            var service = new Mock<IOrderServices>();
            var orderDTOWithProduct = new Mock<OrderDTOWithProduct>().Object;
            service.Setup(x => x.GetById(-1)).Throws(new Exception());
            Assert.ThrowsAsync<Exception>(() => service.Object.GetById(-1));
        }
        [Fact]
        public void GetAllOrdersWithProduct_Ok()
        {
            var service = new Mock<IOrderServices>();
            var productList = new Mock<List<OrderDTOWithProduct>>().Object;
            service.Setup(x => x.GetAllOrdersWithProduct()).Returns(Task.FromResult(new Mock<List<OrderDTOWithProduct>>().Object));
            var result = service.Object.GetAllOrdersWithProduct();
            result.Result.Should().BeEquivalentTo(productList);
        }
        [Fact]
        public void GetAllOrdersWithProduct_Fail()
        {
            var service = new Mock<IOrderServices>();
            var productList = new Mock<List<OrderDTOWithProduct>>().Object;
            service.Setup(x => x.GetAllOrdersWithProduct()).Throws(new Exception());
            Assert.ThrowsAsync<Exception>(() => service.Object.GetAllOrdersWithProduct());
        }
        [Fact]
        public void InsertProductOrder_Ok()
        {
            var service = new Mock<IOrderServices>();
            var productDTOList = new Mock<List<ProductDTO>>().Object;
            var inputList = new Mock<List<ProductIdQtyToInsert>>().Object;
            service.Setup(x => x.InsertProductOrder(inputList, 1))
                .Returns(Task.FromResult(new Mock<List<ProductDTO>>().Object));
            var result = service.Object.InsertProductOrder(inputList, 1);
            result.Result.Should().BeEquivalentTo(productDTOList);
        }
        [Fact]
        public void InsertProductOrder_Fail()
        {
            var service = new Mock<IOrderServices>();
            var productDTOList = new Mock<List<ProductDTO>>().Object;
            var inputList = new Mock<List<ProductIdQtyToInsert>>().Object;
            service.Setup(x => x.InsertProductOrder(inputList, 1)).Throws(new Exception());
            Assert.ThrowsAsync<Exception>(() => service.Object.InsertProductOrder(inputList, 1));
        }
        [Fact]
        public void DeleteProductOrder()
        {
            var service = new Mock<IOrderServices>();
            var products = new Mock<List<ProductIdQtyToInsert>>().Object;
            service.Setup(x => x.DeleteProductOrder(products, 1)).Returns(Task.FromResult(true));
            var result = service.Object.DeleteProductOrder(products, 1);
            Assert.True(result.Result);
        }
    }
}
