using MozoVirtualMicroservicios.Login.Infrastucture;
using MozoVirtualMicroservicios.Login.Infrastucture.Models;

namespace MozoVirtualMicroservicios.Login.Infrastucture
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _dbContext; 
        public EmployeeRepository(EmployeeDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Employee> GetAll()
        {
            try
            {
                var pi = _dbContext.Employees.ToList();
                return pi;
            }
            catch (Exception e)
            {
                var error = e;

                throw;
            }
        }
        public Employee GetByUser(string usuario)
        {
            return _dbContext.Employees.Where(x => x.Usuario == usuario && x.IsActive == 1).FirstOrDefault();
        }
        public Employee GetByToken(string token)
        {
            return _dbContext.Employees.Where(x => x.Token == token).First();
        }

        public Employee GetById(int id)
        {
            return _dbContext.Employees.Find(id);
        }
        public Employee Create(Employee newEmployee)
        {
            var o = _dbContext.Employees.Add(newEmployee).Entity;
            _dbContext.SaveChanges();
            return o;
        }
        public Employee Update(Employee newEmployee)
        {
            var o = _dbContext.Employees.Update(newEmployee).Entity;
            _dbContext.Entry(o).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return o;
        }

    }
}
