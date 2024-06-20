using backend.api.Models;
using backend.api.Models.Generated;

namespace backend.api.Repository.Interfaces
{
    public interface IProductService
    {
        ErrorModel ErrorModel { get; set; }

        void AddProduct(Product product);

        bool DeleteProduct(int id);

        bool EditProduct(Product product);

        IList<Product> GetAllProducts();

        Product? GetProductDetails(int id);
    }
}