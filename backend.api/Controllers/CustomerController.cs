using backend.api.Models.Generated;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(ICustomer customer) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetAllCustomers()
        {
            return new JsonResult(customer.GetAllCustomers());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public IActionResult GetCustomerDetails([FromQuery] int id) //todo: edit this to return 404
        {
            return new JsonResult(customer.GetCustomerDetails(id));
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            var result = customer.AddCustomer(customer);
            return result ? Ok() : BadRequest();
        }

        [HttpPatch]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        public IActionResult EditCustomer([FromBody] Customer customer)
        {
            var result = customer.EditCustomer(customer);
            return result ? Ok() : BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCustomer(int id)
        {
            var result = customer.DeleteCustomer(id);
            return result ? Ok() : BadRequest();
        }
    }
}