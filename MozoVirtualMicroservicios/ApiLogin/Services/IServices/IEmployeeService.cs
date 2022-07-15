using MozoVirtualMicroservicios.Login.Application.DTOs;

namespace MozoVirtualMicroservicios.Login.Services.IServices
{
    public interface IEmployeeService
    {
        public List<EmployeeDTO> GetAll();
        public EmployeeDTO GetById(int id);
        public EmployeeDTO Create(EmployeeDTO newEmployee);
        public EmployeeDTO Update(EmployeeDTO employeeDTO);
        public Task<bool> DeleteById(int id);
        public string Login(string user, string password);
        public EmployeeDTO Validate(string token);
    }
}
