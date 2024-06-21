using backend.api.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace backend.api.Controllers.Interfaces
{
    public interface IProductController
    {
        Task<IActionResult> AddProduct([FromBody, Required] Product Product);
        Task<IActionResult> DeleteProduct(int id);

        Task<IActionResult> EditProduct([FromBody, Required] Product Product);

        IActionResult GetAllProducts();

        IActionResult GetProductDetails(int id);
    }
}