using ApiInventario.DTOs;
using ApiInventario.ExternalServices.IExternalServices;
using ApiInventario.Integrations;
using System.Net;

namespace ApiInventario.ExternalServices
{
    public class ExternalServicesLogin : IExternalServicesLogin
    {
        private readonly IHttpManager _httpManager;

        private const string apiUrlValidateToken = "https://localhost:7090/gateway/Login";

        public ExternalServicesLogin(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public async Task<EmployeeDTO> ValidateToken(string token)
        {
            var url = "https://localhost:7090/gateway/Login";

            var (statusCode, output) = await this._httpManager.GetAsync<EmployeeDTO>(url, token);
            if (statusCode == HttpStatusCode.OK && output != null)
            {
                return output;
            }
            return default;
        }
    }
}
