using ApiMesa.DTO_s;
using ApiMesa.ExternalServices.IExternalServices;
using ApiMesa.Integrations;
using LibreriaWinniePod;
using System.Net;

namespace ApiMesa.ExternalServices
{
    public class ExternalServicesLogin : IExternalServicesLogin
    {
        private readonly IHttpManager _httpManager;

        private const string urlLogin ="https://localhost:7090/gateway/Login";

        public ExternalServicesLogin(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public async Task<EmployeeValidateUser> ValidateToken(string token)
        {
            var (statusCode, output) = await _httpManager.GetAsync<EmployeeValidateUser>(urlLogin, token);
            if (statusCode == HttpStatusCode.OK && output != null)
            {
                return output;
            }
            return default;
        }
    }
}
