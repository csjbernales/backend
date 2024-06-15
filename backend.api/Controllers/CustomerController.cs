using backend.api.Controllers.Interfaces;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace backend.api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class CustomerController(ICustomer dbCustomerModel) : ControllerBase, ICustomerController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetAllCustomers()
        {
            var res = dbCustomerModel.GetAllCustomers();
            return Ok(res);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerDetails(int id)
        {
            Customer? customerInfo = dbCustomerModel.GetCustomerDetails(id);
            if (customerInfo is null)
            {
                return NotFound(dbCustomerModel!.ErrorModel);
            }

            return Ok(customerInfo);
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            dbCustomerModel.AddCustomer(customer);
            return Ok();
        }

        [HttpPatch]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public IActionResult EditCustomer([FromBody] Customer customer)
        {
            bool result = dbCustomerModel.EditCustomer(customer);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(dbCustomerModel.ErrorModel);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCustomer(int id)
        {
            bool result = dbCustomerModel.DeleteCustomer(id);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(dbCustomerModel.ErrorModel);
        }
    }
}