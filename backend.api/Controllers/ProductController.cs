using backend.api.Controllers.Interfaces;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace backend.api.Controllers
{
    /// <summary>
    /// Product API Controller
    /// </summary>
    /// <param name="dbProductModel"></param>
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class ProductController(IProduct dbProductModel) : ControllerBase, IProductController
    {
        /// <summary>
        /// GetAllProducts
        /// </summary>
        /// <returns>All product in a list</returns>
        /// <response code="200">Returns the newly created item</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
        public IActionResult GetAllProducts()
        {
            return Ok(dbProductModel.GetAllProducts());
        }

        /// <summary>
        /// GetProductDetails
        /// </summary>
        /// <param name="id">ID of product to get details of</param>
        /// <returns>The product details specified by id</returns>
        /// <response code="200">Returns the product by id</response>
        /// <response code="404">Returns product not found error model</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public IActionResult GetProductDetails([Required][Range(1, int.MaxValue)] int id)
        {
            Product? ProductInfo = dbProductModel.GetProductDetails(id);
            if (ProductInfo is null)
            {
                return NotFound(dbProductModel!.ErrorModel);
            }

            return Ok(ProductInfo);
        }

        /// <summary>
        /// AddProduct
        /// </summary>
        /// <param name="Product">Contains the product details to be added</param>
        /// <returns>SatusCode 201</returns>
        /// <response code="201">Returns customer not found error model</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddProduct([Required][FromBody] Product Product)
        {
            dbProductModel.AddProduct(Product);
            return Created();
        }

        /// <summary>
        /// EditProduct
        /// </summary>
        /// <param name="Product">Contains the product details to be added</param>
        /// <returns>True if success on updating product detail</returns>
        /// <response code="204">Returns 204 when success</response>
        /// <response code="406">Returns 406 when the request is invalid</response>
        [HttpPatch]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        public IActionResult EditProduct([Required][FromBody] Product Product)
        {
            bool result = dbProductModel.EditProduct(Product);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(406, dbProductModel.ErrorModel);
        }

        /// <summary>
        /// DeleteProduct
        /// </summary>
        /// <param name="id">ID of product to be deleted</param>
        /// <returns>True if success on deleting product detail</returns>
        /// <response code="205">Returns 205 when success</response>
        /// <response code="409 ">Returns 409  when the request is invalid</response>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status205ResetContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public IActionResult DeleteProduct([Required][Range(1, int.MaxValue)] int id)
        {
            bool result = dbProductModel.DeleteProduct(id);
            if (result)
            {
                return StatusCode(205, result);
            }

            return StatusCode(409, dbProductModel.ErrorModel);
        }
    }
}