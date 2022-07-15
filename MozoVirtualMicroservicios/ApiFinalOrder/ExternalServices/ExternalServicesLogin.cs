using ApiFinalOrder.DTO_s;
using ApiFinalOrder.ExternalServices.IExternalServices;
using ApiFinalOrder.Integrations;
using LibreriaWinniePod;
using System.Net;

namespace ApiFinalOrder.ExternalServices
{
    public class ExternalServicesLogin : IExternalServicesLogin
    {
        private readonly IHttpManager _httpManager;        

        public ExternalServicesLogin(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public async Task<EmployeeValidateUser> ValidateToken(string token)
        {
            var url = "https://localhost:7090/gateway/Login";
            
            var (statusCode, output) = await this._httpManager.GetAsync<EmployeeValidateUser>(url, token);
            if (statusCode == HttpStatusCode.OK && output != null)
            {
                return output;
            }
            return default;

        }
    }
}
