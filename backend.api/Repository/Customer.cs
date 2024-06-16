using backend.api.Data.Generated;
using backend.api.Repository.Interfaces;
using System.Text.Json.Serialization;

namespace backend.api.Models.Generated
{
    /// <summary>
    /// Customer model
    /// </summary>
    public partial class Customer : ICustomer
    {
        /// <summary>
        /// Error model property
        /// </summary>
        [JsonIgnore]
        public ErrorModel ErrorModel { get; set; }

        private readonly FullstackDBContext fullstackDBContext;

        /// <summary>
        /// Customer constructor
        /// </summary>
        public Customer()
        {
            ErrorModel = new ErrorModel();
            this.fullstackDBContext ??= new FullstackDBContext();
        }

        /// <summary>
        /// Customer constructor with DI dbContext
        /// </summary>
        public Customer(FullstackDBContext fullstackDBContext)
        {
            this.fullstackDBContext = fullstackDBContext;
            ErrorModel = new ErrorModel();
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>All customers</returns>
        public IList<Customer> GetAllCustomers()
        {
            return [.. fullstackDBContext.Customers];
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id">ID of customer</param>
        /// <returns>The customer details by id</returns>
        public Customer? GetCustomerDetails(int id)
        {
            IQueryable<Customer> customer = fullstackDBContext.Customers.Where(x => x.Id == id);
            if (!customer.Any())
            {
                ErrorModel.ErrorMessage = "Customer not found.";
            }

            return customer.FirstOrDefault();
        }

        /// <summary>
        /// Add customer by adding the customer detail in the request body
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            if (customer.Id == 0)
            {
                fullstackDBContext.Customers.Add(customer);
                fullstackDBContext.SaveChanges();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Payload should not contain 'id' property.";
            }
        }

        /// <summary>
        /// Edit the customer detail by adding the customer detail in the request body
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool EditCustomer(Customer customer)
        {
            int result = 0;
            Customer? customerInfo = fullstackDBContext.Customers.FirstOrDefault(x => x.Id == customer.Id);

            if (customerInfo != null)
            {
                fullstackDBContext.Customers.Update(customer);
                result = fullstackDBContext.SaveChanges();
            }
            else
            {
                ErrorModel.ErrorMessage = $"Customer not found.";
            }

            return result > 0;
        }

        /// <summary>
        /// Delete a customer by id
        /// </summary>
        /// <param name="id">Customer id to be deleted</param>
        /// <returns>True if it succeeded on deletion</returns>
        public bool DeleteCustomer(int id)
        {
            int result = 0;
            IQueryable<Customer> customer = fullstackDBContext.Customers.Where(x => x.Id == id);

            if (customer.Any())
            {
                fullstackDBContext.Customers.Remove(customer.FirstOrDefault()!);
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