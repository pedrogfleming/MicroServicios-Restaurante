using ApiMesa.DTO_s;

namespace ApiMesa.ExternalServices.IExternalServices
{
    public interface IExternalServicesLogin
    {
        public Task<EmployeeValidateUser> ValidateToken(string token);

    }
}
