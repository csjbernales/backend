using backend.api.Customers.Interface;
using backend.data.Models;
using backend.data.Models.Dto;
using backend.data.Models.Generated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace backend.api.Customers
{
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class CustomersController(ICustomerService service) : ControllerBase
    {
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IReadOnlyList<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetAllCustomers()
        {
            return Ok(service.GetAllCustomers());
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerDetails([Required] List<Guid> id)
        {
            List<CustomersDto>? customerInfo = service.GetCustomerDetails(id)!;
            if (customerInfo is null)
            {
                return NotFound(service!.ErrorModel);
            }

            return Ok(customerInfo);
        }

        [HttpPut("Add")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddCustomer([Required][FromBody] List<Customer> customer)
        {
            await service.AddCustomer(customer);
            return Created();
        }

        [HttpPatch("Update")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        public async Task<IActionResult> EditCustomer([Required][FromBody] List<Customer> customer)
        {
            await service.EditCustomer(customer);
            if (service.ErrorModel.IsNullOrEmpty())
            {
                return NoContent();
            }
            return StatusCode(406, service.ErrorModel);
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status205ResetContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteCustomer([Required] List<Guid> id)
        {
            if (service.ErrorModel.IsNullOrEmpty())
            {
                return StatusCode(205);
            }
            return StatusCode(409, service.ErrorModel);
        }
    }
}