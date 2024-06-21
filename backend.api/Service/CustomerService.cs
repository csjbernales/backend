using backend.api.Data.Generated;
using backend.api.Models;
using backend.api.Models.Generated;
using backend.api.Service.Interfaces;

namespace backend.api.Service
{
    public class CustomerService : ICustomerService
    {
        public ErrorModel ErrorModel { get; set; }

        private readonly FullstackDBContext fullstackDBContext;


        public CustomerService()
        {
            ErrorModel = new ErrorModel();
            fullstackDBContext ??= new FullstackDBContext();
        }

        public CustomerService(FullstackDBContext fullstackDBContext)
        {
            this.fullstackDBContext = fullstackDBContext;
            ErrorModel = new ErrorModel();
        }

        public IList<Customer> GetAllCustomers()
        {
            return [.. fullstackDBContext.Customers];
        }

        public Customer? GetCustomerDetails(int id)
        {
            IQueryable<Customer> customer = fullstackDBContext.Customers.Where(x => x.Id == id);
            if (!customer.Any())
            {
                ErrorModel.ErrorMessage = "Customer not found.";
            }

            return customer.FirstOrDefault();
        }

        public async Task AddCustomer(Customer customer)
        {
            if (customer.Id == 0)
            {
                await fullstackDBContext.Customers.AddAsync(customer);
                await fullstackDBContext.SaveChangesAsync();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Payload should not contain 'id' property.";
            }
        }

        public async Task<bool> EditCustomer(Customer customer)
        {
            fullstackDBContext.Customers.Update(customer);
            int result = await fullstackDBContext.SaveChangesAsync();
            if (result != 0)
            {
                ErrorModel.ErrorMessage = $"Customer not found.";
            }

            return result > 0;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            int result = 0;
            IQueryable<Customer> customer = fullstackDBContext.Customers.Where(x => x.Id == id);

            if (customer.Any())
            {
                fullstackDBContext.Customers.Remove(customer.FirstOrDefault()!);
                result = await fullstackDBContext.SaveChangesAsync();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Failed to delete customer with ID '{id}'. ID may not exist.";
            }

            return result > 0;
        }
    }
}