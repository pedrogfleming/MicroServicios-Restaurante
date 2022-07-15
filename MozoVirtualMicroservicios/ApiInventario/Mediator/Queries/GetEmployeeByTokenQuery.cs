using ApiInventario.DTOs;
using MediatR;

namespace ApiInventario.Queries
{
    public class GetEmployeeByTokenQuery : IRequest<EmployeeDTO>
    {
        public string token { get; set; }

        public GetEmployeeByTokenQuery(string token)
        {
            this.token = token;
        }
    }
}
