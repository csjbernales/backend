using backend.api.Data.Generated;
using backend.api.Service;
using Xunit.Priority;
using PriorityAttribute = Xunit.Priority.PriorityAttribute;

namespace backend.api.Models.Generated.Tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class ProductsServiceTests
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

            ProductsService sut = new(fullstackDBContext);

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

            ProductsService sut = new(fullstackDBContext);

            Product? allProducts = sut.GetProductDetails(2);

            allProducts.Should().BeSameAs(cust);
        }

        [Fact()]
        public async Task C_AddProductTest()
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
            await fullstackDBContext.Products.AddAsync(cust);
            await fullstackDBContext.SaveChangesAsync();

            ProductsService sut = new(fullstackDBContext);

            await sut.AddProduct(cust);
            Product? allProducts = sut.GetProductDetails(3);

            allProducts.Should().BeSameAs(cust);
        }

        [Fact()]
        public async Task D_EditProductTest()
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
            await fullstackDBContext.AddAsync(cust);
            await fullstackDBContext.SaveChangesAsync();

            Product? updatedCust = fullstackDBContext.Products.Where(x => x.Id == cust.Id).FirstOrDefaultAsync().Result;

            updatedCust!.Quantity = 26;

            ProductsService sut = new(fullstackDBContext);

            Task<bool> allProducts = sut.EditProduct(updatedCust);

            allProducts.Result.Should().BeTrue();
        }

        [Fact()]
        public async Task E_DeleteProductTest()
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
            await fullstackDBContext.Products.AddAsync(cust);
            await fullstackDBContext.SaveChangesAsync();

            ProductsService sut = new(fullstackDBContext);

            bool del = await sut.DeleteProduct(5);

            Product? allProducts = sut.GetProductDetails(5);
            allProducts.Should().Be(null);
            del.Should().BeTrue();
        }
    }
}