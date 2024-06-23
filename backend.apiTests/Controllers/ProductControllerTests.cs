using backend.api.Models.Generated;
using backend.api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Controllers.Tests
{
    public class ProductControllerTests
    {
        private readonly IProductService product;
        public ProductControllerTests()
        {
            product = A.Fake<IProductService>();
        }

        [Fact()]
        public void GetAllProductsTest()
        {
            Product productInfo = new()
            {
                Id = 1,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };

            IList<Product> products = [
                    productInfo
                ];

            A.CallTo(() => product.GetAllProducts()).Returns(products);
            ProductsController sut = new(product);
            IActionResult res = sut.GetAllProducts();

            OkObjectResult? okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            IList<Product>? returnedProducts = okresult.Value as IList<Product>;
            returnedProducts.Should().NotBeNull();
            returnedProducts.Should().HaveCount(1);
            Product returnedProduct = returnedProducts![0];
            returnedProduct.Should().BeEquivalentTo(productInfo);
        }

        [Fact()]
        public void GetProductDetailsTest()
        {
            Product productInfo = new()
            {
                Id = 1,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };

            A.CallTo(() => this.product.GetProductDetails(A<int>.Ignored)).Returns(productInfo);
            ProductsController sut = new(product);
            IActionResult res = sut.GetProductDetails(1);

            OkObjectResult? okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            Product? returnedProducts = okresult.Value as Product;
            returnedProducts.Should().NotBeNull();
            returnedProducts.Should().BeEquivalentTo(productInfo);
        }

        [Fact()]
        public async Task AddProductTest()
        {
            Product productInfo = new()
            {
                Id = 1,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };

            ProductsController sut = new(product);
            IActionResult res = await sut.AddProduct(productInfo);

            res.Should().NotBeNull();
        }

        [Fact()]
        public async Task EditProductTest()
        {
            Product productInfo = new()
            {
                Id = 1,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };

            A.CallTo(() => this.product.EditProduct(A<Product>.Ignored)).Returns(true);
            ProductsController sut = new(product);
            IActionResult res = await sut.EditProduct(productInfo);

            NoContentResult? okresult = res as NoContentResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(204);
        }

        [Fact()]
        public async Task DeleteProductTest()
        {
            A.CallTo(() => this.product.DeleteProduct(A<int>.Ignored)).Returns(true);
            ProductsController sut = new(product);
            IActionResult res = await sut.DeleteProduct(1);

            ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(res);
            Assert.Equal(205, statusCodeResult.StatusCode);
        }
    }
}