using backend.api.Data.Generated;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Service.Interfaces;

namespace backend.api.Service
{
    public class ProductService : IProductService
    {
        public ErrorModel ErrorModel { get; set; }

        private readonly FullstackDBContext fullstackDBContext;

        public ProductService()
        {
            ErrorModel = new ErrorModel();
            fullstackDBContext ??= new FullstackDBContext();
        }

        public ProductService(FullstackDBContext fullstackDBContext)
        {
            this.fullstackDBContext = fullstackDBContext;
            ErrorModel = new ErrorModel();
        }

        public IList<Product> GetAllProducts()
        {
            return [.. fullstackDBContext.Products];
        }

        public Product? GetProductDetails(int id)
        {
            IQueryable<Product> product = fullstackDBContext.Products.Where(x => x.Id == id);
            if (!product.Any())
            {
                ErrorModel.ErrorMessage = "Product not found.";
            }

            return product.FirstOrDefault();
        }

        public async Task AddProduct(Product product)
        {
            if (product.Id == 0)
            {
                await fullstackDBContext.Products.AddAsync(product);
                await fullstackDBContext.SaveChangesAsync();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Payload should not contain 'id' property.";
            }
        }

        public async Task<bool> EditProduct(Product product)
        {
            fullstackDBContext.Products.Update(product);
            int result = await fullstackDBContext.SaveChangesAsync();

            if (result != 0)
            {
                ErrorModel.ErrorMessage = $"Product not found.";
            }
            return result > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            int result = 0;
            IQueryable<Product> product = fullstackDBContext.Products.Where(x => x.Id == id);

            if (product.Any())
            {
                fullstackDBContext.Products.Remove(product.FirstOrDefault()!);
                result = await fullstackDBContext.SaveChangesAsync();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Failed to delete product with ID '{id}'. ID may not exist.";
            }

            return result > 0;
        }
    }
}