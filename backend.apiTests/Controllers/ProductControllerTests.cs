using backend.api.Data.Generated;
using backend.api.Models.Generated;
using backend.api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace backend.api.Controllers.Tests
{
    public class ProductControllerTests
    {
        private readonly IProduct product;
        public ProductControllerTests()
        {
            product = A.Fake<IProduct>();
        }

        [Fact()]
        public void GetAllProductsTest()
        {
            var productInfo = new Product
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
            var sut = new ProductController(product);
            var res = sut.GetAllProducts();

            var okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            var returnedProducts = okresult.Value as IList<Product>;
            returnedProducts.Should().NotBeNull();
            returnedProducts.Should().HaveCount(1);
            var returnedProduct = returnedProducts![0];
            returnedProduct.Should().BeEquivalentTo(productInfo);
        }

        [Fact()]
        public void GetProductDetailsTest()
        {
            var productInfo = new Product
            {
                Id = 1,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };

            A.CallTo(() => this.product.GetProductDetails(A<int>.Ignored)).Returns(productInfo);
            var sut = new ProductController(product);
            var res = sut.GetProductDetails(1);

            var okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            var returnedProducts = okresult.Value as Product;
            returnedProducts.Should().NotBeNull();
            returnedProducts.Should().BeEquivalentTo(productInfo);
        }

        [Fact()]
        public void AddProductTest()
        {
            var productInfo = new Product
            {
                Id = 1,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };

            var sut = new ProductController(product);
            var res = sut.AddProduct(productInfo);

            res.Should().NotBeNull();
        }

        [Fact()]
        public void EditProductTest()
        {
            var productInfo = new Product
            {
                Id = 1,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };

            A.CallTo(() => this.product.EditProduct(A<Product>.Ignored)).Returns(true);
            var sut = new ProductController(product);
            var res = sut.EditProduct(productInfo);

            var okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);
        }

        [Fact()]
        public void DeleteProductTest()
        {
            A.CallTo(() => this.product.DeleteProduct(A<int>.Ignored)).Returns(true);
            var sut = new ProductController(product);
            var res = sut.DeleteProduct(1);

            var okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);
        }
    }
}