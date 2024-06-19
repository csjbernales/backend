using backend.api.Models;
using backend.api.Models.Generated;

namespace backend.api.Repository.Interfaces
{
    public interface IProduct
    {
        string Category { get; set; }

        ErrorModel ErrorModel { get; set; }

        int Id { get; set; }

        string Name { get; set; }

        int Quantity { get; set; }

        void AddProduct(Product product);

        bool DeleteProduct(int id);

        bool EditProduct(Product product);

        IList<Product> GetAllProducts();

        Product? GetProductDetails(int id);
    }
}