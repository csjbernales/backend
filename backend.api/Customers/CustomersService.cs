using backend.api.Customers.Interface;
using backend.data.Data.Generated;
using backend.data.Models;
using backend.data.Models.Dto;
using backend.data.Models.Generated;
using Mapster;

namespace backend.api.Customers
{
    public class CustomersService : ICustomerService
    {
        public List<ErrorModel> ErrorModel { get; set; }

        private readonly FullstackDBContext fullstackDBContext;

        public CustomersService(FullstackDBContext fullstackDBContext)
        {
            this.fullstackDBContext = fullstackDBContext;
            ErrorModel = [];
        }

        public IReadOnlyList<CustomersDto> GetAllCustomers()
        {
            return fullstackDBContext.Customers.ToList().Adapt<IReadOnlyList<CustomersDto>>();
        }

        public List<CustomersDto> GetCustomerDetails(List<Guid> ids)
        {
            List<CustomersDto> customers = [];
            ids.ForEach(y =>
            {
                CustomersDto single = fullstackDBContext.Customers.Where(x => x.Id == y).SingleOrDefault().Adapt<CustomersDto>();

                if (single.Id == Guid.Empty)
                {
                    customers.Add(single);
                }
                else
                {
                    ErrorModel.Add(new ErrorModel()
                    {
                        ErrorMessage = $"Customer with ID {y} not found."
                    });
                }
            });

            return customers;
        }

        public async Task AddCustomer(List<Customer> customer)
        {
            foreach (Customer item in customer)
            {
                if (item.Id == Guid.Empty)
                {
                    await fullstackDBContext.Customers.AddAsync(item);
                    await fullstackDBContext.SaveChangesAsync();
                }
                else
                {
                    ErrorModel.Add(new ErrorModel()
                    {
                        ErrorMessage = $"Error has occured for id {item.Id}"
                    });
                }
            }
        }

        public async Task EditCustomer(List<Customer> customer)
        {
            foreach (Customer item in customer)
            {
                int result = 0;
                fullstackDBContext.Customers.Update(item);
                result += await fullstackDBContext.SaveChangesAsync();
                if (result != 0)
                {
                    ErrorModel.Add(new ErrorModel()
                    {
                        ErrorMessage = $"Customer {item.Id} not found."
                    });
                }
            }
        }

        public async Task DeleteCustomer(List<Guid> id)
        {
            foreach (Guid item in id)
            {
                IQueryable<Customer> customer = fullstackDBContext.Customers.Where(x => x.Id == item);

                if (customer.Any())
                {
                    fullstackDBContext.Customers.Remove(customer.FirstOrDefault()!);
                    await fullstackDBContext.SaveChangesAsync();
                }
                else
                {
                    ErrorModel.Add(new ErrorModel()
                    {
                        ErrorMessage = $"Failed to delete customer with ID '{item}'. ID may not exist."
                    });
                }
            }
        }
    }
}