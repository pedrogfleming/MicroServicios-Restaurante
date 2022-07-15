using MediatR;

namespace ApiMesa.Commands
{
    public class CerrarMesaCommand : IRequest<bool>
    {
        public CerrarMesaCommand(int mesaId)
        {
            MesaId = mesaId;
        }

        public int MesaId { get; set; }
    }
}
