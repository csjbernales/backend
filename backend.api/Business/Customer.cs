using backend.api.Data;
using backend.api.Models.Generated;

namespace backend.api.Models.Generated
{
    public partial class Customer(FullstackdbContext context) : ICustomer
    {
        public IList<Customer> GetAllCustomers()
        {
            return [..context.Customers];
        }

        public Customer? GetCustomerDetails(int id)
        {
            return context.Customers.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool AddCustomer(Customer customer)
        {
            context.Add(customer);
            var result = context.SaveChanges();

            return result != 0;
        }

        public bool EditCustomer(Customer customer)
        {
            context.Customers.Update(customer);
            var result = context.SaveChanges();

            return result != 0;
        }

        public bool DeleteCustomer(int id)
        {
            context.Remove(id);
            var result = context.SaveChanges();

            return result != 0;
        }
    }
}
