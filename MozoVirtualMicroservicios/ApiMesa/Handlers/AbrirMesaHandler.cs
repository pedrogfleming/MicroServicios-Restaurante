using ApiMesa.Commands;
using ApiMesa.Services.IServices;
using MediatR;

namespace ApiMesa.Handlers
{
    public class AbrirMesaHandler : IRequestHandler<AbrirMesaCommand, bool>
    {
        private readonly IMesaServices _service;

        public AbrirMesaHandler(IMesaServices service)
        {
            _service = service;
        }

        public Task<bool> Handle(AbrirMesaCommand request, CancellationToken cancellationToken)
        {
            return _service.AbrirMesa(request.OrderId, request.MesaId);
        }
    }
}
