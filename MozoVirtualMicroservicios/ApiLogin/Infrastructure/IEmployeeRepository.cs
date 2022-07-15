using MozoVirtualMicroservicios.Login.Infrastucture.Models;

namespace MozoVirtualMicroservicios.Login.Infrastucture
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetAll();
        public Employee GetByUser(string usuario);
        public Employee GetById(int id);
        public Employee GetByToken(string token);
        public Employee Create(Employee newEmployee);
        public Employee Update(Employee newEmployee);
    }
}
