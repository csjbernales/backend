﻿using backend.api.Customers.Interface;
using backend.data.Data.Generated;
using backend.data.Models;
using backend.data.Models.Dto;
using backend.data.Models.Generated;
using Mapster;

namespace backend.api.Customers
{
    public class CustomersService : ICustomerService
    {
        public ErrorModel ErrorModel { get; set; }

        private readonly FullstackDBContext fullstackDBContext;

        public CustomersService()
        {
            ErrorModel = new ErrorModel();
            fullstackDBContext ??= new FullstackDBContext();
        }

        public CustomersService(FullstackDBContext fullstackDBContext)
        {
            this.fullstackDBContext = fullstackDBContext;
            ErrorModel = new ErrorModel();
        }

        public IReadOnlyList<CustomersDto> GetAllCustomers()
        {
            return fullstackDBContext.Customers.ToList().Adapt<IReadOnlyList<CustomersDto>>();
        }

        public CustomersDto? GetCustomerDetails(Guid id)
        {
            IQueryable<Customer> customer = fullstackDBContext.Customers.Where(x => x.Id == id);
            if (!customer.Any())
            {
                ErrorModel.ErrorMessage = "Customer not found.";
            }

            return customer.FirstOrDefault().Adapt<CustomersDto>();
        }

        public async Task AddCustomer(Customer customer)
        {
            if (customer.Id == Guid.Empty)
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

        public async Task<bool> DeleteCustomer(Guid id)
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