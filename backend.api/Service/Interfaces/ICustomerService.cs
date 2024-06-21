using backend.api.Models;
using backend.api.Models.Generated;

namespace backend.api.Service.Interfaces
{
    public interface ICustomerService
    {
        ErrorModel ErrorModel { get; set; }

        Task AddCustomer(Customer customer);

        Task<bool> DeleteCustomer(int id);

        Task<bool> EditCustomer(Customer customer);

        IList<Customer> GetAllCustomers();

        Customer? GetCustomerDetails(int id);
    }
}