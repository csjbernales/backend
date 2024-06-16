using backend.api.Models;
using backend.api.Models.Generated;

namespace backend.api.Repository.Interfaces
{
    /// <summary>
    /// Product interface
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Category property
        /// </summary>
        string Category { get; set; }

        /// <summary>
        /// ErrorModel property
        /// </summary>
        ErrorModel ErrorModel { get; set; }

        /// <summary>
        /// Id property
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Name property
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Quantity property
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product">Product value received from request body</param>
        void AddProduct(Product product);

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>True if update succeeded</returns>
        bool DeleteProduct(int id);

        /// <summary>
        /// Edit product
        /// </summary>
        /// <param name="product">Product value received from request body</param>
        /// <returns>True if update succeeded</returns>
        bool EditProduct(Product product);

        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns>List of all products</returns>
        IList<Product> GetAllProducts();

        /// <summary>
        /// Get product details by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns></returns>
        Product? GetProductDetails(int id);
    }
}