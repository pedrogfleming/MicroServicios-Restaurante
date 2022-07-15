using LibreriaWinniePod;
using MozoVirtualMicroservicios.Login.Application.DTOs;
using MozoVirtualMicroservicios.Login.Infrastucture;
using MozoVirtualMicroservicios.Login.Infrastucture.Models;
using MozoVirtualMicroservicios.Login.Services.IServices;
using System.Text;
using XSystem.Security.Cryptography;

namespace MozoVirtualMicroservicios.Login.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<bool> DeleteById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            employee.IsActive = 0;
            _employeeRepository.Update(employee);
            if (employee.IsActive > 0)
            {                
                return true;
            }
            else
            {
                return false;
            }
        }

        public EmployeeDTO Create(EmployeeDTO newEmployee)
        {
            newEmployee.Token =  GenerateToken(newEmployee.Usuario, newEmployee.Password);
            var employee = _employeeRepository.Create(MappeadorGenerico.Map<Employee>(newEmployee));
            return MappeadorGenerico.Map<EmployeeDTO>(employee);
        }

        public List<EmployeeDTO> GetAll()
        {
            var employeeList= _employeeRepository.GetAll();
            return MappeadorGenerico.MapEntities<EmployeeDTO>(employeeList);
        }

        public EmployeeDTO GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return MappeadorGenerico.Map<EmployeeDTO>(employee);
        }

        public string Login(string user, string password)
        {
            var employee = _employeeRepository.GetByUser(user);
            if (CheckInput(employee))
            {
               return employee.Password == password ? employee.Token : "Usuario y/o password incorrecto";               
            }
            else 
            {
                return "Usuario y/o password incorrecto";
            }
        }

        private bool CheckInput(Employee employee)
        {
            if (employee != null)
            {
                if (!string.IsNullOrWhiteSpace(employee.Password))
                {
                    return true;
                }
                else return false;
            }
            else return false;          
        }

        private string GenerateToken(string user, string password)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", true, true)
                      .AddJsonFile($"appsettings.{env}.json", true)
                      .Build();

            var secret = configuration.GetSection("ServiceExternal").GetSection("Secret").Value;
            var token = user + secret + password;
            return GetStringSha256Hash(token);
        }
        internal static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public EmployeeDTO Update(EmployeeDTO employeeDTO)
        {
            var employee = _employeeRepository.Update(MappeadorGenerico.Map<Employee>(employeeDTO));

            return MappeadorGenerico.Map<EmployeeDTO>(employee);
        }

        public EmployeeDTO Validate(string token)
        {
            var employee = _employeeRepository.GetByToken(token);

            return MappeadorGenerico.Map<EmployeeDTO>(employee);
        }
    }
}
