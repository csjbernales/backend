using backend.api.Controllers.Interfaces;
using backend.api.Service.Interfaces;
using backend.data.Models;
using backend.data.Models.Dto;
using backend.data.Models.Generated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace backend.api.Controllers
{
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class CustomersController(ICustomerService service) : ControllerBase, ICustomerController
    {
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IList<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetAllCustomers()
        {
            return Ok(service.GetAllCustomers());
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerDetails([Required][Range(1, int.MaxValue)] int id)
        {
            CustomersDto customerInfo = service.GetCustomerDetails(id)!;
            if (customerInfo is null)
            {
                return NotFound(service!.ErrorModel);
            }

            return Ok(customerInfo);
        }

        [HttpPut("Add")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddCustomer([Required][FromBody] Customer customer)
        {
            await service.AddCustomer(customer);
            return Created();
        }

        [HttpPatch("Update")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        public async Task<IActionResult> EditCustomer([Required][FromBody] Customer customer)
        {
            bool result = await service.EditCustomer(customer);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(406, service.ErrorModel);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status205ResetContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteCustomer([Required][Range(1, int.MaxValue)] int id)
        {
            bool result = await service.DeleteCustomer(id);
            if (result)
            {
                return StatusCode(205, result);
            }

            return StatusCode(409, service.ErrorModel);
        }
    }
}