using backend.api.Data;
using backend.api.Models.Generated;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace backend.api.Models.Generated
{
    public partial class Customer(FullstackDBContext fullstackDBContext) : ICustomer
    {
        [JsonIgnore]
        public ErrorModel ErrorModel { get; set; } = new ErrorModel();

        public IList<Customer> GetAllCustomers()
        {
            return [.. fullstackDBContext.Customers];
        }

        public Customer? GetCustomerDetails(int id)
        {
            var customer = fullstackDBContext.Customers.Where(x => x.Id == id).FirstOrDefault();
            if(customer is null)
            {
                ErrorModel.ErrorMessage = "Customer not found.";
            }

            return customer;
        }

        public bool AddCustomer(Customer customer)
        {
            fullstackDBContext.Customers.Add(customer);
            var result = fullstackDBContext.SaveChanges();

            if(result == 0)
            {
                ErrorModel.ErrorMessage = $"Failed to add new customer {customer.Firstname} {customer.Lastname}";
            }
            return result > 0;
        }

        public bool EditCustomer(Customer customer)
        {
            fullstackDBContext.Customers.Update(customer);
            var result = fullstackDBContext.SaveChanges();

            if (result == 0)
            {
                ErrorModel.ErrorMessage = $"Failed to update customer {customer.Firstname} {customer.Lastname}";
            }
            return result > 0;
        }

        public bool DeleteCustomer(int id)
        {
            int result = 0;
            var customer = fullstackDBContext.Customers.Where(x => x.Id == id);

            if(customer.Any())
            {
                fullstackDBContext.Customers.Remove(customer.First());
                result = fullstackDBContext.SaveChanges();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Failed to delete customer with ID {id}";
            }

            return result > 0;
        }
    }
}
