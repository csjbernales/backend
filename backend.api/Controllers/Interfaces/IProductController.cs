using backend.api.Models.Generated;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Controllers.Interfaces
{
    /// <summary>
    /// Product Controller interface
    /// </summary>
    public interface IProductController
    {
        /// <summary>
        /// AddProduct
        /// </summary>
        /// <param name="Product">Contains the product details to be added</param>
        /// <returns>SatusCode 200</returns>
        IActionResult AddProduct([FromBody] Product Product);

        /// <summary>
        /// DeleteProduct
        /// </summary>
        /// <param name="id">ID of product to be deleted</param>
        /// <returns>True if success on deleting product detail</returns>
        IActionResult DeleteProduct(int id);

        /// <summary>
        /// EditProduct
        /// </summary>
        /// <param name="Product">Contains the product details to be added</param>
        /// <returns>True if success on updating product detail</returns>
        IActionResult EditProduct([FromBody] Product Product);

        /// <summary>
        /// GetAllProducts
        /// </summary>
        /// <returns>All product in a list</returns>
        IActionResult GetAllProducts();

        /// <summary>
        /// GetProductDetails
        /// </summary>
        /// <param name="id">ID of product to get details of</param>
        /// <returns>The product details specified by id</returns>
        IActionResult GetProductDetails(int id);
    }
}