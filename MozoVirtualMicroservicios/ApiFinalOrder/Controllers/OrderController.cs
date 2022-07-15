using ApiFinalOrder.Input;
using ApiFinalOrder.Mediator.Commands;
using ApiFinalOrder.Mediator.Queries;
using ApiMesa.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinalOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] string token)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Mozo }).Result)
            {
                return Unauthorized();
            }
            var query = new GetAllOrdersWithProductQuery();
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id, [FromHeader] string token)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Mozo,ERoles.Cocinero }).Result)
            {
                return Unauthorized();
            }
            var query = new GetOrderByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrder([FromBody] OrderToInsertInput input, [FromHeader] string token)
        {
            var isValidRol = await IsValidRol(token, new List<ERoles>() { ERoles.Mozo });
            if (!isValidRol)
            {
                return Unauthorized();
            }
            input.Token = token;
            var query = new InsertOrderCommand(input);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderState(
            [FromHeader] string token,
            [FromBody] OrderStateToUpdate input)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Mozo, ERoles.Cocinero }).Result)
            {
                return Unauthorized();
            }
            var query = new UpdateOrderStateCommand(input,token);
            var result = _mediator.Send(query);
            return await result != null ? Ok(result) : BadRequest();
        }

        [HttpPut("AddProductToOrder/{orderId}")]
        public async Task<IActionResult> AddProductToOrder([FromBody] List<ProductToInsertInput> input, [FromHeader] string token, int orderId)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Mozo }).Result)
            {
                return Unauthorized();
            }
            var query = new AddProductToOrderCommand(input, orderId,token);
            var result =  await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPut("RemoveProductFromOrder/{orderId}")]
        public async Task<IActionResult> RemoveProductFromOrder(
            [FromBody] List<ProductToInsertInput> input, 
            [FromHeader] string token,
            int orderId)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Mozo }).Result)
            {
                return Unauthorized();
            }
            var query = new RemoveProductFromOrderCommand(input, orderId,token);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> IsValidRol(string t, List<ERoles> eRoles)
        {
            if (!string.IsNullOrWhiteSpace(t) &&
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
        [HttpGet("TotalFee")]
        public async Task<IActionResult> GetTotalFee([FromHeader] string token, int waiterId)
        {
            var query = new GetTotalFeeQuery(waiterId);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPut("Pay")]
        public async Task<IActionResult> Pay(
            [FromHeader] string token,
            [FromBody] PayOrderCommand command)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Mozo }).Result)
            {
                return Unauthorized();
            }
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : BadRequest();
        }

    }
}

