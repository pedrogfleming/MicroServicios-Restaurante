using ApiFinalOrder.Domain.Model;
using ApiFinalOrder.Infrastructure.IRepository;

namespace OrdersAPITests
{
    [Trait("Category", "Repositories")]
    public class Repositories
    {
        [Fact]
        public void GetAll_Not_Empty()
        {
            var orderRepo = new Mock<IOrderRepository>();

            ICollection<Order> orders = new List<Order>
                    {
                        new Mock<Order>().Object,
                        new Mock<Order>().Object
                    };

            orderRepo.Setup(x => x.GetAll())
                .Returns(Task.FromResult(orders).Result);

            var result = orderRepo.Object.GetAll();

            Assert.NotEmpty(result);
        }
        [Fact]
        public void GetAll_Empty()
        {
            var orderRepo = new Mock<IOrderRepository>();

            ICollection<Order> orders = new List<Order>();

            orderRepo.Setup(x => x.GetAll())
                .Returns(Task.FromResult(orders).Result);

            var result = orderRepo.Object.GetAll();

            Assert.Empty(result);
        }
        [Fact]
        public void Update_Order_Equal()
        {
            var orderRepo = new Mock<IOrderRepository>();
            var order = new Mock<Order>().Object;

            orderRepo.Setup(x => x.Update(order)).Returns(order);


            var result = orderRepo.Object.Update(order);

            Assert.Equal(order, result);
        }
        [Fact]
        public void Update_Order_Not_Equal()
        {
            var orderRepo = new Mock<IOrderRepository>();
            var order = new Mock<Order>().Object;

            orderRepo.Setup(x => x.Update(new Mock<Order>().Object)).Returns(order);

            var result = orderRepo.Object.Update(order);

            Assert.NotEqual(order, result);
        }


        [Fact]
        public void Create_Order_Equal()
        {

        }
    }
}