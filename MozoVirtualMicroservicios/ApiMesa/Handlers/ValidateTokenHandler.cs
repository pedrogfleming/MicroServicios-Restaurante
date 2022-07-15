using ApiMesa.DTO_s;
using ApiMesa.ExternalServices.IExternalServices;
using ApiMesa.Queries;
using MediatR;

namespace ApiMesa.Mediator.Handler
{
    public class ValidateTokenHandler : IRequestHandler<GetEmployeeByTokenQuery, EmployeeValidateUser>
    {
        private readonly IExternalServicesLogin _ExternalServicesLogin;

        public ValidateTokenHandler(IExternalServicesLogin externalServicesLogin)
        {
            _ExternalServicesLogin = externalServicesLogin;
        }

        public Task<EmployeeValidateUser> Handle(GetEmployeeByTokenQuery request, CancellationToken cancellationToken)
        {
            return _ExternalServicesLogin.ValidateToken(request.token);
        }
    }
}
