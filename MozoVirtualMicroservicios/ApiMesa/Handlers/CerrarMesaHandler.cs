using ApiMesa.Commands;
using ApiMesa.Services.IServices;
using MediatR;

namespace ApiMesa.Handlers
{
    public class CerrarMesaHandler : IRequestHandler<CerrarMesaCommand, bool>
    {
        private IMesaServices _serviceMesa;

        public CerrarMesaHandler(IMesaServices serviceMesa)
        {
            _serviceMesa = serviceMesa;
        }


        public Task<bool> Handle(CerrarMesaCommand request, CancellationToken cancellationToken)
        {
            return _serviceMesa.CerrarMesa(request.MesaId);
        }
    }
}
