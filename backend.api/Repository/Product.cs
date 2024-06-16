using backend.api.Data.Generated;
using backend.api.Repository.Interfaces;
using System.Text.Json.Serialization;

namespace backend.api.Models.Generated
{
    /// <summary>
    /// Product class
    /// </summary>
    public partial class Product : IProduct
    {
        /// <summary>
        /// Error model property
        /// </summary>
        [JsonIgnore]
        public ErrorModel ErrorModel { get; set; }

        private readonly FullstackDBContext fullstackDBContext;

        /// <summary>
        /// Product controller
        /// </summary>
        public Product()
        {
            ErrorModel = new ErrorModel();
            this.fullstackDBContext ??= new FullstackDBContext();
        }


        /// <summary>
        /// Product constructor with DI dbContext
        /// </summary>
        public Product(FullstackDBContext fullstackDBContext)
        {
            this.fullstackDBContext = fullstackDBContext;
            ErrorModel = new ErrorModel();
        }


        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>All products</returns>
        public IList<Product> GetAllProducts()
        {
            return [.. fullstackDBContext.Products];
        }


        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">ID of product</param>
        /// <returns>The product details by id</returns>
        public Product? GetProductDetails(int id)
        {
            IQueryable<Product> product = fullstackDBContext.Products.Where(x => x.Id == id);
            if (!product.Any())
            {
                ErrorModel.ErrorMessage = "Product not found.";
            }

            return product.FirstOrDefault();
        }

        /// <summary>
        /// Add product by adding the product detail in the request body
        /// </summary>
        /// <param name="product"></param>
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


        /// <summary>
        /// Edit the product detail by adding the product detail in the request body
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool EditProduct(Product product)
        {
            int result = 0;
            Product? customerInfo = fullstackDBContext.Products.FirstOrDefault(x => x.Id == product.Id);

            if (customerInfo != null)
            {
                fullstackDBContext.Products.Update(customerInfo);
                result = fullstackDBContext.SaveChanges();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Customer not found.";
            }

            return result > 0;
        }


        /// <summary>
        /// Delete a product by id
        /// </summary>
        /// <param name="id">Customer id to be deleted</param>
        /// <returns>True if it succeeded on deletion</returns>
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