using MediatR;

namespace ApiMesa.Commands
{
    public class AbrirMesaCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
        public int MesaId { get; set; }

        public AbrirMesaCommand(int orderId, int mesaId)
        {
            OrderId = orderId;
            MesaId = mesaId;
        }

        
    }
}
