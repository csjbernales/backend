using backend.api.Models;
using backend.api.Models.Generated;

namespace backend.api.Repository.Interfaces
{
    public interface ICustomerService
    {
        ErrorModel ErrorModel { get; set; }

        void AddCustomer(Customer customer);

        bool DeleteCustomer(int id);

        bool EditCustomer(Customer customer);

        IList<Customer> GetAllCustomers();

        Customer? GetCustomerDetails(int id);
    }
}