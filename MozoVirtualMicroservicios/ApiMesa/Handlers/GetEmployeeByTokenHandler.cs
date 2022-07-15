using ApiMesa.DTO_s;
using ApiMesa.ExternalServices.IExternalServices;
using ApiMesa.Queries;
using MediatR;

namespace ApiMesa.Handlers
{
    public class GetEmployeeByTokenHandler : IRequestHandler<GetEmployeeByTokenQuery, EmployeeValidateUser>
    {
        private readonly IExternalServicesLogin _ExternalServicesLogin;

        public GetEmployeeByTokenHandler(IExternalServicesLogin externalServicesLogin)
        {
            _ExternalServicesLogin = externalServicesLogin;
        }

        public async Task<EmployeeValidateUser> Handle(GetEmployeeByTokenQuery request, CancellationToken cancellationToken)
        {
            return await _ExternalServicesLogin.ValidateToken(request.token);
        }
    }
}
