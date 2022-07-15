using ApiMesa.DTO_s;
using MediatR;

namespace ApiMesa.Queries
{
    public class GetEmployeeByTokenQuery :IRequest<EmployeeValidateUser>
    {
        public string token { get; }

        public GetEmployeeByTokenQuery(string token)
        {
            this.token = token;
        }
    }
}
