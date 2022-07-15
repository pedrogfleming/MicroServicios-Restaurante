using ApiFinalOrder.DTO_s;

namespace ApiFinalOrder.ExternalServices.IExternalServices
{
    public interface IExternalServicesLogin
    {
        public Task<EmployeeValidateUser> ValidateToken(string token);

    }
}
