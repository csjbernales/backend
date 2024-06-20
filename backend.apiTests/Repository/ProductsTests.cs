using backend.api.Data.Generated;
using Xunit.Priority;
using PriorityAttribute = Xunit.Priority.PriorityAttribute;

namespace backend.api.Models.Generated.Tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class ProductsTests
    {
        private DbContextOptions<FullstackDBContext> dbContextOptions = new();

        [Fact, Priority(0)]
        public void A_GetAllProductsTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            fullstackDBContext.Products.Add(
                new Product
                {
                    Id = 1,
                    Name = "Item name",
                    Category = "Food",
                    Quantity = 10

                });
            fullstackDBContext.SaveChanges();

            Product sut = new(fullstackDBContext);

            IList<Product> allProducts = sut.GetAllProducts();

            allProducts.Should().HaveCount(1);
        }

        [Fact()]
        public void B_GetProductDetailsTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Product cust = new()
            {
                Id = 2,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };
            fullstackDBContext.Add(cust);
            fullstackDBContext.SaveChanges();

            Product sut = new(fullstackDBContext);

            Product? allProducts = sut.GetProductDetails(2);

            allProducts.Should().BeSameAs(cust);
        }

        [Fact()]
        public void C_AddProductTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Product cust = new()
            {
                Id = 3,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };
            fullstackDBContext.Products.Add(cust);
            fullstackDBContext.SaveChanges();

            Product sut = new(fullstackDBContext);

            sut.AddProduct(cust);
            Product? allProducts = sut.GetProductDetails(3);

            allProducts.Should().BeSameAs(cust);
        }

        [Fact()]
        public void D_EditProductTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Product cust = new()
            {
                Id = 4,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };
            fullstackDBContext.Add(cust);
            fullstackDBContext.SaveChanges();

            Product? updatedCust = fullstackDBContext.Products.Where(x => x.Id == cust.Id).FirstOrDefault();

            updatedCust!.Quantity = 26;

            Product sut = new(fullstackDBContext);

            bool allProducts = sut.EditProduct(updatedCust);

            allProducts.Should().BeTrue();
        }

        [Fact()]
        public void E_DeleteProductTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Product cust = new()
            {
                Id = 5,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };
            fullstackDBContext.Products.Add(cust);
            fullstackDBContext.SaveChanges();

            Product sut = new(fullstackDBContext);

            bool del = sut.DeleteProduct(5);

            Product? allProducts = sut.GetProductDetails(5);
            allProducts.Should().Be(null);
            del.Should().BeTrue();
        }
    }
}