using ApiMesa.Domain;
using ApiMesa.Domain.Models;
using ApiMesa.DTO_s;
using ApiMesa.Infrastructure;
using ApiMesa.Services.IServices;
using LibreriaWinniePod;

namespace ApiMesa.Services
{
    public class MesaServices : IMesaServices
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public MesaServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AbrirMesa(int orderId, int mesaId)
        {
            var order = _unitOfWork.Orders.GetById(orderId);
            var orderMesa = new Mesa_Order()
            {
                OrderId = orderId,
                WaiterId = order.WaiterId,
                MesaId = mesaId,
                QuantityOfPerson = order.Customers
            };
            var isAvailMesa = _unitOfWork.Mesas.GetById(mesaId);
            if (!isAvailMesa.IsAvailable)
            {
                throw new MesaNotAvailableException($"La mesa {mesaId} no esta disponible");
            }
            isAvailMesa.IsAvailable = false;
            _unitOfWork.Mesas.Update(isAvailMesa);
            var result = _unitOfWork.Order_Mesas.Insert(orderMesa);            
            if (result != null)
            {
                _unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CerrarMesa(int idMesa)
        {
            var mesa = _unitOfWork.Mesas.GetById(idMesa);
            mesa.IsAvailable = true;
            var result = _unitOfWork.Mesas.Update(mesa);
            if (result != null)
            {
                _unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Task<List<MesaDTO>> VerMesasDisponibles()
        {
            //commit
            var result = _unitOfWork.Mesas.GetAll().ToList();
            IEnumerable<MesaDTO> mesas = result.Where(x => x.IsAvailable == true).
                Select(c=> MappeadorGenerico.Map<MesaDTO>(c));

            return Task.FromResult(mesas.ToList());
        }
    }
}
