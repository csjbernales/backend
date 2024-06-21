using backend.api.Models;
using backend.api.Models.Generated;

namespace backend.api.Service.Interfaces
{
    public interface IProductService
    {
        ErrorModel ErrorModel { get; set; }

        Task AddProduct(Product product);

        Task<bool> DeleteProduct(int id);

        Task<bool> EditProduct(Product product);

        IList<Product> GetAllProducts();

        Product? GetProductDetails(int id);
    }
}