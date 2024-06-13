using backend.api.Models;
using backend.api.Models.Generated;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(ICustomer dbCustomerModel) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetAllCustomers()
        {
            return new JsonResult(dbCustomerModel.GetAllCustomers());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerDetails(int id)
        {
            var customerInfo = dbCustomerModel.GetCustomerDetails(id);
            if(customerInfo is null)
            {
                return new JsonResult(dbCustomerModel!.ErrorModel);
            }

            return new JsonResult(customerInfo);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            var result = dbCustomerModel.AddCustomer(customer);
            if (result)
            {
                return new JsonResult(result);
            }

            return new JsonResult(dbCustomerModel.ErrorModel);

        }

        [HttpPatch]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public IActionResult EditCustomer([FromBody] Customer customer)
        {
            var result = dbCustomerModel.EditCustomer(customer);
            if (result)
            {
                return new JsonResult(result);
            }

            return new JsonResult(dbCustomerModel.ErrorModel);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCustomer(int id)
        {
            var result = dbCustomerModel.DeleteCustomer(id);
            if (result)
            {
                return new JsonResult(result);
            }

            return new JsonResult(dbCustomerModel.ErrorModel);
        }
    }
}