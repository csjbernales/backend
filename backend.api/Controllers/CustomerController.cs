using backend.api.Controllers.Interfaces;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace backend.api.Controllers
{
    /// <summary>
    /// Customer API Controller
    /// </summary>
    /// <param name="dbCustomerModel"></param>
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class CustomerController(ICustomer dbCustomerModel) : ControllerBase, ICustomerController
    {
        /// <summary>
        /// GetAllCustomers
        /// </summary>
        /// <returns>All customer in a list</returns>
        /// <response code="200">Returns All customer</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetAllCustomers()
        {
            return Ok(dbCustomerModel.GetAllCustomers());
        }

        /// <summary>
        /// GetCustomerDetails
        /// </summary>
        /// <param name="id">ID of customer to get details of</param>
        /// <returns>The customer details specified by id</returns>
        /// <response code="200">Returns the customer by id</response>
        /// <response code="404">Returns customer not found error model</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerDetails([Required][Range(1, int.MaxValue)] int id)
        {
            Customer? customerInfo = dbCustomerModel.GetCustomerDetails(id);
            if (customerInfo is null)
            {
                return NotFound(dbCustomerModel!.ErrorModel);
            }

            return Ok(customerInfo);
        }

        /// <summary>
        /// AddCustomer
        /// </summary>
        /// <param name="customer">Contains the customer details to be added</param>
        /// <returns>SatusCode 201</returns>
        /// <response code="201">Returns customer not found error model</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddCustomer([Required][FromBody] Customer customer)
        {
            dbCustomerModel.AddCustomer(customer);
            return Created();
        }

        /// <summary>
        /// EditCustomer
        /// </summary>
        /// <param name="customer">Contains the customer details to be added</param>
        /// <returns>True if success on updating customer detail</returns>
        /// <response code="204">Returns 204 when success</response>
        /// <response code="406">Returns 406 when the request is invalid</response>
        [HttpPatch]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        public IActionResult EditCustomer([Required][FromBody] Customer customer)
        {
            bool result = dbCustomerModel.EditCustomer(customer);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(406, dbCustomerModel.ErrorModel);
        }

        /// <summary>
        /// DeleteCustomer
        /// </summary>
        /// <param name="id">ID of customer to be deleted</param>
        /// <returns>True if success on deleting customer detail</returns>
        /// <response code="205">Returns 205 when success</response>
        /// <response code="409 ">Returns 409 when the request is invalid</response>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status205ResetContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public IActionResult DeleteCustomer([Required][Range(1, int.MaxValue)] int id)
        {
            bool result = dbCustomerModel.DeleteCustomer(id);
            if (result)
            {
                return StatusCode(205, result);
            }

            return StatusCode(409, dbCustomerModel.ErrorModel);
        }
    }
}