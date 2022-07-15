using ApiMesa.DTO_s;
using ApiMesa.Queries;
using ApiMesa.Services.IServices;
using MediatR;

namespace ApiMesa.Handlers
{
    public class TraerMesasDisponiblesHandler : IRequestHandler<TraerMesasDisponiblesQuery, List<MesaDTO>>
    {
        private readonly IMesaServices _mesaService;

        public TraerMesasDisponiblesHandler(IMesaServices mesaService)
        {
            _mesaService = mesaService;
        }

        public Task<List<MesaDTO>> Handle(TraerMesasDisponiblesQuery request, CancellationToken cancellationToken)
        {
            return _mesaService.VerMesasDisponibles();
        }
    }
}
