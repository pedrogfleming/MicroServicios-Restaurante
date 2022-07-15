using ApiLogin.Application.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MozoVirtualMicroservicios.Login.Application.DTOs;
using MozoVirtualMicroservicios.Login.Services.IServices;

namespace MozoVirtualMicroservicios.Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public LoginController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = _employeeService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet()]
        public async Task<IActionResult> Validate([FromHeader] string token)
        {
            var result = _employeeService.Validate(token);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost("Auth")]
        public IActionResult Login([FromBody] UserInput user)
        {
            if(!string.IsNullOrWhiteSpace(user.Usuario) || !string.IsNullOrWhiteSpace(user.Password))
            {
                var result = _employeeService.Login(user.Usuario, user.Password);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest("Usuario/password incorrecto");
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] EmployeeDTO employeeDto)
        {
            var result = _employeeService.Update(employeeDto);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO newEmployee)
        {
            var result = _employeeService.Create(newEmployee);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _employeeService.DeleteById(id));
        }
       
    }
}
