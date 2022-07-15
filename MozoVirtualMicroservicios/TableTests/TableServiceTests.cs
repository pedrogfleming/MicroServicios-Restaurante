using ApiMesa.Domain.Models;
using ApiMesa.DTO_s;
using ApiMesa.Infrastructure;
using ApiMesa.Services;
using ApiMesa.Services.IServices;
using LibreriaWinniePod;
using Moq;
using MozoVirtualMicroservicios.Login.Infrastucture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableTests
{
    public class TableServiceTests
    {
        //commit
        [Fact]
        public async void AbrirMesaOK()
        {
            var repo = new Mock<IUnitOfWork>();
            var service = new Mock<IMesaServices>();


            var mesa = new Mesa()
            {
                Id = 1,
                Codigo = "5",
                IsAvailable = true,
                MaxCustomer = 5
            };

            repo.Setup(x => x.Mesas.Insert(mesa)).Returns(mesa);
            bool flag = mesa.IsAvailable;
            service.Setup(x => x.AbrirMesa(1, 1)).Returns(Task.FromResult(false));
            var flag2 = service.Object.AbrirMesa(1, 1).Result;

            Assert.NotEqual(flag, flag2);
        }
        [Fact]
        public async void AbrirMesaFail()
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
            service.Setup(x => x.AbrirMesa(1, 1)).Returns(Task.FromResult(true));
            var flag2 = service.Object.AbrirMesa(1, 1).Result;

            Assert.Equal(flag, flag2);
        }
        [Fact]
        public async void CerrarMesaOK() 
        {
            var repo = new Mock<IUnitOfWork>();
            var service = new Mock<IMesaServices>();
            
            var mesa = new Mesa()
            {
                Id = 1,
                Codigo = "5",
                IsAvailable = false,
                MaxCustomer = 5
            };
            repo.Setup(x => x.Mesas.Insert(mesa)).Returns(mesa);
            var flag = mesa.IsAvailable;
            service.Setup(x => x.CerrarMesa(1)).Returns(Task.FromResult<bool>(true));
            var flag2 = service.Object.CerrarMesa(1).Result;

            Assert.NotEqual(flag, flag2);
        }
        [Fact]
        public async void CerrarMesaFail() 
        {
            var repo = new Mock<IUnitOfWork>();
            var service = new Mock<IMesaServices>();

            var mesa = new Mesa()
            {
                Id = 1,
                Codigo = "5",
                IsAvailable = false,
                MaxCustomer = 5
            };
            repo.Setup(x => x.Mesas.Insert(mesa)).Returns(mesa);
            var flag = mesa.IsAvailable;
            service.Setup(x => x.CerrarMesa(1)).Returns(Task.FromResult<bool>(false));
            var flag2 = service.Object.CerrarMesa(1).Result;

            Assert.Equal(flag, flag2);
        }
        [Fact]
        public async void VerMesasDisponiblesOK() 
        {
            var service = new Mock<IMesaServices>();
            var repo = new Mock<IUnitOfWork>();
            List<MesaDTO> mesas = new List<MesaDTO>();
            var a = new MesaDTO() { Id = 1, Codigo = "10", IsAvailable = true};
            var b = new MesaDTO() { Id = 2, Codigo = "11", IsAvailable = false};
            var c = new MesaDTO() { Id = 3, Codigo = "12", IsAvailable = true};
            var d = new MesaDTO() { Id = 4, Codigo = "13", IsAvailable = false};
            mesas.Add(a);
            mesas.Add(b);
            mesas.Add(c);
            mesas.Add(d);
            var mesasDisp = mesas.Where(x => x.IsAvailable == true).ToList();
            var res = service.Object.VerMesasDisponibles().Result;

            Assert.NotEqual(res, mesasDisp);
        }
        [Fact]
        public async void VerMesasDisponiblesFail()
        {
            var service = new Mock<IMesaServices>();
            var repo = new Mock<IUnitOfWork>();
            List<MesaDTO> mesas = new List<MesaDTO>();
            var a = new MesaDTO() { Id = 1, Codigo = "10", IsAvailable = true };
            var b = new MesaDTO() { Id = 2, Codigo = "11", IsAvailable = false };
            var c = new MesaDTO() { Id = 3, Codigo = "12", IsAvailable = true };
            var d = new MesaDTO() { Id = 4, Codigo = "13", IsAvailable = false };
            mesas.Add(a);
            mesas.Add(b);
            mesas.Add(c);
            mesas.Add(d);
            var res = service.Object.VerMesasDisponibles().Result;

            Assert.NotEqual(res, mesas);
        }
    }
}
