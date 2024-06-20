using backend.api.Data.Generated;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Repository.Interfaces;

namespace backend.api.Repository
{
    public class ProductService : IProductService
    {
        public ErrorModel ErrorModel { get; set; }

        private readonly FullstackDBContext fullstackDBContext;

        public ProductService()
        {
            ErrorModel = new ErrorModel();
            this.fullstackDBContext ??= new FullstackDBContext();
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

        public void AddProduct(Product product)
        {
            if (product.Id == 0)
            {
                fullstackDBContext.Products.Add(product);
                fullstackDBContext.SaveChanges();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Payload should not contain 'id' property.";
            }
        }

        public bool EditProduct(Product product)
        {
            int result = 0;
            fullstackDBContext.Products.Update(product);
            result = fullstackDBContext.SaveChanges();

            if (result != 0)
            {
                ErrorModel.ErrorMessage = $"Product not found.";
            }
            return result > 0;
        }

        public bool DeleteProduct(int id)
        {
            int result = 0;
            IQueryable<Product> product = fullstackDBContext.Products.Where(x => x.Id == id);

            if (product.Any())
            {
                fullstackDBContext.Products.Remove(product.FirstOrDefault()!);
                result = fullstackDBContext.SaveChanges();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Failed to delete product with ID '{id}'. ID may not exist.";
            }

            return result > 0;
        }
    }
}