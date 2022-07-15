using ApiFinalOrder.DTO_s;
using ApiFinalOrder.ExternalServices.IExternalServices;
using ApiFinalOrder.Mediator.Queries;
using MediatR;

namespace ApiFinalOrder.Mediator.Handler
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
