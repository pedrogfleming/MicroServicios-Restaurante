using ApiMesa.Commands;
using ApiMesa.Domain.Enums;
using ApiMesa.DTO_s;
using ApiMesa.Input;
using ApiMesa.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiMesa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MesaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Abrir")]
        public async Task<IActionResult> Abrir([FromBody] AbrirMesaInput input, [FromHeader] string token)
        {
            if (!IsValidRol(token, new List<ERoles>() {ERoles.Mozo }).Result)
            {
                return Unauthorized();
            }
            var command = new AbrirMesaCommand(input.OrderId, input.MesaId);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : BadRequest();

        }

        [HttpPut("Cerrar/{id}")]
        public async Task<IActionResult> Cerrar(int id, [FromHeader] string token)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Mozo }).Result)
            {
                return Unauthorized();
            }
            var command = new CerrarMesaCommand(id);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll([FromHeader] string token)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Mozo }).Result)
            {
                return Unauthorized();
            }
            var query = new TraerMesasDisponiblesQuery();
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> IsValidRol(string t, List<ERoles> eRoles)
        {
            if(!string.IsNullOrWhiteSpace(t) &&
                t != "Usuario/password incorrecto" &&
                t != "No se pudo validar el usuario")
            {
                //Get the data from the employee
                var employeeDTO = await _mediator.Send(new GetEmployeeByTokenQuery(t));
                //Validate the rol of the employee
                return eRoles.Any(x => ((int)x) == employeeDTO.Rol);
            }  
            return false;
        }
    }
}
