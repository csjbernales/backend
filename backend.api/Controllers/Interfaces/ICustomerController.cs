using backend.api.Models.Generated;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Controllers.Interfaces
{
    /// <summary>
    /// Customer controller interface
    /// </summary>
    public interface ICustomerController
    {
        /// <summary>
        /// AddCustomer
        /// </summary>
        /// <param name="customer">Contains the customer details to be added</param>
        /// <returns>SatusCode 200</returns>
        IActionResult AddCustomer([FromBody] Customer customer);

        /// <summary>
        /// DeleteCustomer
        /// </summary>
        /// <param name="id">ID of customer to be deleted</param>
        /// <returns>True if success on deleting customer detail</returns>
        IActionResult DeleteCustomer(int id);

        /// <summary>
        /// EditCustomer
        /// </summary>
        /// <param name="customer">Contains the customer details to be added</param>
        /// <returns>True if success on updating customer detail</returns>
        IActionResult EditCustomer([FromBody] Customer customer);

        /// <summary>
        /// GetAllCustomers
        /// </summary>
        /// <returns>All customer in a list</returns>
        IActionResult GetAllCustomers();

        /// <summary>
        /// GetCustomerDetails
        /// </summary>
        /// <param name="id">ID of customer to get details of</param>
        /// <returns>The customer details specified by id</returns>
        IActionResult GetCustomerDetails(int id);
    }
}