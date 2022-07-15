using ApiMesa.Domain.Models;
using ApiMesa.Infrastructure;
using ApiMesa.Services;
using ApiMesa.Services.IServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TableTest
{
    public class TableServiceTests
    {
        [Fact]
        public async void AbrirMesa()
        {
            var repo = new Mock<IUnitOfWork>();
            var service = new Mock<IMesaServices>();
           

            var mesa = new Mesa() 
            {
                Id = 1,
                Codigo = "20",
                IsAvailable = true,
                MaxCustomer = 5 
            };

            repo.Setup(x => x.Mesas.Insert(mesa)).Returns(mesa);
            bool flag = mesa.IsAvailable;
            var flag2 = service.Setup(x => x.AbrirMesa(1, 1)).Returns(Task.FromResult(false));

            Assert.NotEqual(flag, flag2);


            //var m = new MesaServices(repo.Object);
            //var r = m.AbrirMesa(1, 1);


        }
    }
}
