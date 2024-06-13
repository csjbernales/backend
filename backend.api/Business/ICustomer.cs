
namespace backend.api.Models.Generated
{
    public interface ICustomer
    {
        string Age { get; set; }
        string Firstname { get; set; }
        int Id { get; set; }
        bool IsEmployed { get; set; }
        string? Lastname { get; set; }
        string? Middlename { get; set; }
        string Sex { get; set; }

        bool AddCustomer(Customer customer);
        bool DeleteCustomer(int id);
        bool EditCustomer(Customer customer);
        IList<Customer> GetAllCustomers();
        Customer? GetCustomerDetails(int id);
    }
}