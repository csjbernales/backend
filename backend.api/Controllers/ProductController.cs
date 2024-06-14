using backend.api.Controllers.Interfaces;
using backend.api.Models;
using backend.api.Models.Generated;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProduct dbProductModel) : ControllerBase, IProductController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
        public IActionResult GetAllProducts()
        {
            return new JsonResult(dbProductModel.GetAllProducts());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public IActionResult GetProductDetails(int id)
        {
            Product? ProductInfo = dbProductModel.GetProductDetails(id);
            if (ProductInfo is null)
            {
                return new JsonResult(dbProductModel!.ErrorModel);
            }

            return new JsonResult(ProductInfo);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddProduct([FromBody] Product Product)
        {
            dbProductModel.AddProduct(Product);
            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public IActionResult EditProduct([FromBody] Product Product)
        {
            bool result = dbProductModel.EditProduct(Product);
            if (result)
            {
                return new JsonResult(result);
            }

            return new JsonResult(dbProductModel.ErrorModel);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteProduct(int id)
        {
            bool result = dbProductModel.DeleteProduct(id);
            if (result)
            {
                return new JsonResult(result);
            }

            return new JsonResult(dbProductModel.ErrorModel);
        }
    }
}