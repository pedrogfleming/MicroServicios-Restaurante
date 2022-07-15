using ApiMesa.DTO_s;
using MediatR;

namespace ApiMesa.Queries
{
    public class TraerMesasDisponiblesQuery : IRequest<List<MesaDTO>>
    {
    }
}
