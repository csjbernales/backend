using backend.api.Models.Generated;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Controllers.Interfaces
{
    public interface IProductController
    {
        IActionResult AddProduct([FromBody] Product Product);
        IActionResult DeleteProduct(int id);
        IActionResult EditProduct([FromBody] Product Product);
        IActionResult GetAllProducts();
        IActionResult GetProductDetails(int id);
    }
}