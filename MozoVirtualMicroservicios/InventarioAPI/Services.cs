using ApiInventario.Services.IService;

namespace InventarioAPITests
{
    [Trait("Category", "Services")]
    public class Services
    {
        [Fact]
        public void GetAll_Ok()
        {
            var service = new Mock<IProductService>();
            var productList = new Mock<List<ProductDTO>>().Object;
            productList.Add(new Mock<ProductDTO>().Object);
            productList.Add(new Mock<ProductDTO>().Object);
            service.Setup(x => x.GetAll()).Returns(Task.FromResult((IEnumerable<ProductDTO>)productList));
            var result = service.Object.GetAll();
            Assert.NotEmpty(result.Result);
        }
        [Fact]
        public void GetAll_Fail()
        {
            var service = new Mock<IProductService>();
            var productList = new Mock<IEnumerable<ProductDTO>>().Object;
            productList.Append(new Mock<ProductDTO>().Object);
            productList.Append(null);
            service.Setup(x => x.GetAll()).Returns(Task.FromResult(productList));
            var result = service.Object.GetAll();
            Assert.ThrowsAsync<Exception>(() => service.Object.GetAll());
        }
        [Fact]
        public void Update_Ok()
        {
            var service = new Mock<IProductService>();
            var inputDTO = new Mock<ProductDTO>().Object;
            var outputDTO = new Mock<ProductDTO>().Object;
            service.Setup(x => x.Update(inputDTO)).Returns(Task.FromResult(outputDTO));
            var result = service.Object.Update(inputDTO);
            result.Result.Should().BeEquivalentTo(inputDTO);
        }
        [Fact]
        public void Update_Fail()
        {
            var service = new Mock<IProductService>();
            var otherDTO = new Mock<object>().Object;
            Assert.ThrowsAsync<Exception>(() => service.Object.Update((ProductDTO)otherDTO));
        }
        //[Fact]
        //public void Insert_OK()
        //{
        //    var service = new Mock<IProductService>();
        //    var productDTO = new Mock<ProductDTO>().Object;
        //    service.Setup(x => x.Insert(productDTO)).Returns(Task.FromResult(productDTO));
        //    var result = service.Object.Insert(productDTO);
        //    result.Result.Should().BeEquivalentTo(productDTO);
        //}
        //[Fact]
        //public void Insert_Fail()
        //{
        //    var service = new Mock<IProductService>();
        //    var randomDTO = new Mock<object>().Object;
        //    Assert.ThrowsAsync<Exception>(() => service.Object.Insert((ProductDTO)randomDTO));
        //}
        [Fact]
        public void Delete_Ok()
        {
            var service = new Mock<IProductService>();
            service.Setup(x => x.Delete(1)).Returns(Task.FromResult(true));
            var result = service.Object.Delete(1);
            Assert.IsType(typeof(bool), result.Result);
        }
        [Fact]
        public void Delete_Fail()
        {
            var service = new Mock<IProductService>();
            service.Setup(x => x.Delete(1)).Returns(Task.FromResult(true));
            Assert.ThrowsAsync<Exception>(() => service.Object.Delete(-1));
        }
        [Theory]
        [InlineData(1, 1.0d, true)]
        [InlineData(-10, -2.50d, false)]
        public void UpdateCost_Ok(int category, decimal cost, bool outcome)
        {
            var service = new Mock<IProductService>();
            service.Setup(x => x.UpdateCost(category, cost)).Returns(Task.FromResult(outcome));
            var result = service.Object.UpdateCost(category, cost);
            result.Result.Should().Be(outcome);
        }
    }
}