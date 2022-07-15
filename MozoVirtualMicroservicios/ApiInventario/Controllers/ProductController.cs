using ApiInventario.Commands;
using ApiInventario.Domain.IModels;
using ApiInventario.DTOs;
using ApiInventario.Input;
using ApiInventario.Queries;
using ApiMesa.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiInventario.Controllers
{
    [Route("api/inventario")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Solo espera ser utilizado cuando se modifica una Order
        /// antes de ser tomada por un cocinero
        /// </summary>
        /// <param name="products">Id,Qty</param>
        /// <returns>bool: Resultado actualizacion en db</returns>
        [HttpPut("UpdatedOrder")]
        public async Task<IActionResult> UpdateStockByUpdatedOrder(
            [FromHeader] string token,
            [FromBody] List<ProductToInsertInput> products)
        {
            var command = new UpdateStockByUpdatedOrderCommand(products);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(
            [FromHeader] string token, 
            [FromBody] ProductToAddInput productDto)
        {
            var command = new AddProductCommand(productDto);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            [FromHeader] string token,
            [FromBody] ProductDTO productDto)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Manager }).Result)
            {
                return Unauthorized();
            }
            var command = new UpdateProductCommand(productDto);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromHeader] string token)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Manager }).Result)
            {
                return Unauthorized();
            }
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPut("updateCost")]
        public async Task<IActionResult> UpdateCost(
            [FromHeader] string token,
            [FromBody]ProductUpdateCostInput productToUpdate)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Manager }).Result)
            {
                return Unauthorized();
            }
            var command = new UpdateProductCostCommand(productToUpdate.Category, productToUpdate.Cost);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromHeader] string token,int id)
        {
            if (!IsValidRol(token, new List<ERoles>() { ERoles.Manager }).Result)
            {
                return Unauthorized();
            }
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);
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
    }
}
