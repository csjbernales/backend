using backend.api.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace backend.api.Controllers.Interfaces
{
    public interface ICustomerController
    {
        Task<IActionResult> AddCustomer([FromBody, Required] Customer customer);

        Task<IActionResult> DeleteCustomer(int id);

        Task<IActionResult> EditCustomer([FromBody, Required] Customer customer);

        IActionResult GetAllCustomers();

        IActionResult GetCustomerDetails(int id);
    }
}