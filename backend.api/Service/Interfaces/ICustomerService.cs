using backend.data.Models;
using backend.data.Models.Dto;
using backend.data.Models.Generated;

namespace backend.api.Service.Interfaces
{
    public interface ICustomerService
    {
        ErrorModel ErrorModel { get; set; }

        Task AddCustomer(Customer customer);

        Task<bool> DeleteCustomer(int id);

        Task<bool> EditCustomer(Customer customer);

        IList<CustomersDto> GetAllCustomers();

        CustomersDto? GetCustomerDetails(int id);
    }
}