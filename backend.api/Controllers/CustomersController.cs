using backend.api.Controllers.Interfaces;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Service.Interfaces;
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
        [HttpGet]
        [ProducesResponseType(typeof(IList<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetAllCustomers()
        {
            return Ok(service.GetAllCustomers());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerDetails([Required][Range(1, int.MaxValue)] int id)
        {
            Customer? customerInfo = service.GetCustomerDetails(id);
            if (customerInfo is null)
            {
                return NotFound(service!.ErrorModel);
            }

            return Ok(customerInfo);
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddCustomer([Required][FromBody] Customer customer)
        {
            service.AddCustomer(customer);
            return Created();
        }

        [HttpPatch]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        public IActionResult EditCustomer([Required][FromBody] Customer customer)
        {
            bool result = service.EditCustomer(customer);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(406, service.ErrorModel);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status205ResetContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public IActionResult DeleteCustomer([Required][Range(1, int.MaxValue)] int id)
        {
            bool result = service.DeleteCustomer(id);
            if (result)
            {
                return StatusCode(205, result);
            }

            return StatusCode(409, service.ErrorModel);
        }
    }
}