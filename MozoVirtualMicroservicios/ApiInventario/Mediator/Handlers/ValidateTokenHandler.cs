using ApiInventario.DTOs;
using ApiInventario.ExternalServices.IExternalServices;
using ApiInventario.Queries;
using MediatR;

namespace ApiInventario.Mediator.Handler
{
    public class ValidateTokenHandler : IRequestHandler<GetEmployeeByTokenQuery, EmployeeDTO>
    {
        private readonly IExternalServicesLogin _ExternalServicesLogin;

        public ValidateTokenHandler(IExternalServicesLogin externalServicesLogin)
        {
            _ExternalServicesLogin = externalServicesLogin;
        }

        public Task<EmployeeDTO> Handle(GetEmployeeByTokenQuery request, CancellationToken cancellationToken)
        {
            return _ExternalServicesLogin.ValidateToken(request.token);
        }
    }
}
