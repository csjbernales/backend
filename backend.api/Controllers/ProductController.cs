using backend.api.Controllers.Interfaces;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace backend.api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    public class ProductController(IProduct dbProductModel) : ControllerBase, IProductController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
        public IActionResult GetAllProducts()
        {
            return Ok(dbProductModel.GetAllProducts());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public IActionResult GetProductDetails(int id)
        {
            Product? ProductInfo = dbProductModel.GetProductDetails(id);
            if (ProductInfo is null)
            {
                return NotFound(dbProductModel!.ErrorModel);
            }

            return Ok(ProductInfo);
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddProduct([FromBody] Product Product)
        {
            dbProductModel.AddProduct(Product);
            return Ok();
        }

        [HttpPatch]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public IActionResult EditProduct([FromBody] Product Product)
        {
            bool result = dbProductModel.EditProduct(Product);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(dbProductModel.ErrorModel);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteProduct(int id)
        {
            bool result = dbProductModel.DeleteProduct(id);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(dbProductModel.ErrorModel);
        }
    }
}