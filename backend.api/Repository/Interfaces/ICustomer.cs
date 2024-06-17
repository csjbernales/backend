using backend.api.Models;
using backend.api.Models.Generated;

namespace backend.api.Repository.Interfaces
{
    /// <summary>
    /// ICustomer
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// Age
        /// </summary>
        int? Age { get; set; }

        /// <summary>
        /// Firstname
        /// </summary>
        string Firstname { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// IsEmployed
        /// </summary>
        bool IsEmployed { get; set; }

        /// <summary>
        /// Lastname
        /// </summary>
        string? Lastname { get; set; }

        /// <summary>
        /// Middlename
        /// </summary>
        string? Middlename { get; set; }

        /// <summary>
        /// Sex
        /// </summary>
        string Sex { get; set; }

        /// <summary>
        /// ErrorModel
        /// </summary>
        ErrorModel ErrorModel { get; set; }

        /// <summary>
        /// customer
        /// </summary>
        /// <param name="customer">customer</param>
        void AddCustomer(Customer customer);

        /// <summary>
        /// DeleteCustomer
        /// </summary>
        /// <param name="id">)</param>
        /// <returns></returns>
        bool DeleteCustomer(int id);

        /// <summary>
        /// EditCustomer
        /// </summary>
        /// <param name="customer">customer</param>
        /// <returns>boolean</returns>
        bool EditCustomer(Customer customer);

        /// <summary>
        /// GetAllCustomers
        /// </summary>
        /// <returns>list of customers</returns>
        IList<Customer> GetAllCustomers();

        /// <summary>
        /// GetCustomerDetails
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Customer?</returns>
        Customer? GetCustomerDetails(int id);
    }
}