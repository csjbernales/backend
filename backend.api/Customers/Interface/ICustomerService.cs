using backend.data.Models;
using backend.data.Models.Dto;
using backend.data.Models.Generated;

namespace backend.api.Customers.Interface
{
    public interface ICustomerService
    {
        List<ErrorModel> ErrorModel { get; set; }

        Task AddCustomer(List<Customer> customer);
        Task DeleteCustomer(List<Guid> id);
        Task EditCustomer(List<Customer> customer);
        IReadOnlyList<CustomersDto> GetAllCustomers();
        List<CustomersDto> GetCustomerDetails(List<Guid> ids);
    }
}