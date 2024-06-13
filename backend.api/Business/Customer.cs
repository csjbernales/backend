using backend.api.Data.Generated;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace backend.api.Models.Generated
{
    public partial class Customer : ICustomer
    {
        [JsonIgnore]
        public ErrorModel ErrorModel { get; set; }

        private readonly FullstackDBContext fullstackDBContext;

        public Customer()
        {
            ErrorModel = new ErrorModel();
            this.fullstackDBContext ??= new FullstackDBContext();
        }

        public Customer(FullstackDBContext fullstackDBContext)
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

            return customer.First();
        }

        public void AddCustomer(Customer customer)
        {
            fullstackDBContext.Customers.Add(customer);
            fullstackDBContext.SaveChanges();
        }

        public bool EditCustomer(Customer customer)
        {
            int result = 0;
            try
            {
                fullstackDBContext.Customers.Update(customer);
                result = fullstackDBContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                ErrorModel.ErrorMessage = $"{exception.Message}";
            }

            return result > 0;
        }

        public bool DeleteCustomer(int id)
        {
            int result = 0;
            IQueryable<Customer> customer = fullstackDBContext.Customers.Where(x => x.Id == id);

            if (customer.Any())
            {
                fullstackDBContext.Customers.Remove(customer.First());
                result = fullstackDBContext.SaveChanges();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Failed to delete customer with ID '{id}'. ID may not exist.";
            }

            return result > 0;
        }
    }
}