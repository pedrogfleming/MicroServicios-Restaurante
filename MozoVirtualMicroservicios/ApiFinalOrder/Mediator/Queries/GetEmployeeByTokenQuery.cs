using ApiFinalOrder.DTO_s;
using MediatR;

namespace ApiFinalOrder.Mediator.Queries
{
    public class GetEmployeeByTokenQuery : IRequest<EmployeeValidateUser>
    {
        public string token { get; set; }

        public GetEmployeeByTokenQuery(string token)
        {
            this.token = token;
        }
    }
}
