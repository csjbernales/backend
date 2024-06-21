using backend.api.Controllers.Interfaces;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace backend.api.Controllers
{
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class ProductsController(IProductService dbProductModel) : ControllerBase, IProductController
    {
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
        public IActionResult GetAllProducts()
        {
            return Ok(dbProductModel.GetAllProducts());
        }

        [HttpGet("Get/{id}")]
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

        [HttpPut("Add")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddProduct([Required][FromBody] Product Product)
        {
            await dbProductModel.AddProduct(Product);
            return Created();
        }

        [HttpPatch("Update")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        public async Task<IActionResult> EditProduct([Required][FromBody] Product Product)
        {
            bool result = await dbProductModel.EditProduct(Product);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(406, dbProductModel.ErrorModel);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status205ResetContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteProduct([Required][Range(1, int.MaxValue)] int id)
        {
            bool result = await dbProductModel.DeleteProduct(id);
            if (result)
            {
                return StatusCode(205, result);
            }

            return StatusCode(409, dbProductModel.ErrorModel);
        }
    }
}