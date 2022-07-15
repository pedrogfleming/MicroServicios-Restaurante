using ApiInventario.DTOs;

namespace ApiInventario.ExternalServices.IExternalServices
{
    public interface IExternalServicesLogin
    {
        public Task<EmployeeDTO> ValidateToken(string token);

    }
}
