using backend.api.Models.Generated;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Controllers.Interfaces
{
    public interface ICustomerController
    {
        IActionResult AddCustomer([FromBody] Customer customer);
        IActionResult DeleteCustomer(int id);
        IActionResult EditCustomer([FromBody] Customer customer);
        IActionResult GetAllCustomers();
        IActionResult GetCustomerDetails(int id);
    }
}