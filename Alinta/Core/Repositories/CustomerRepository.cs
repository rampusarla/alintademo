using Alinta.Core.Messages;
using Alinta.Core.Infrastructure;
using Alinta.Core.Interfaces;
using Alinta.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Alinta.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerDBContext _context;
        private readonly ILogger<CustomerRepository> _logger;
        public CustomerRepository(CustomerDBContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Customer>> GetAllCustomers(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var updatedSearchString = searchString.Trim().ToUpper();
                return await _context.Customers.Where(c => c.FirstName.Trim().ToUpper().Contains(updatedSearchString) || c.LastName.Trim().ToUpper().Contains(updatedSearchString)).ToListAsync();
            }

            return await _context.Customers.ToListAsync();
        }
        //To Add new customer record     
        public async Task<int> AddCustomer(CustomerViewModel customerVM)
        {                        
            //Determine the next ID
            var newID = _context.Customers.Select(x => x.Id).Max() + 1;
            var customer = new Customer()
            {
                Id = newID,
                FirstName = customerVM.FirstName,
                LastName = customerVM.LastName,
                DateOfBirth = customerVM.DateOfBirth
            };            

            _context.Customers.Add(customer);
            _logger.LogInformation(InformationMessages.CustomerAddedSuccessfully, customer.Id, JsonConvert.SerializeObject(customer));
            return await _context.SaveChangesAsync();            
        }
        //To Update the records of a particluar customer
        public async Task<int> UpdateCustomer(int id, CustomerViewModel customerVM)
        {
            var customer = await GetCustomerData(id);

            customer.FirstName = customerVM.FirstName;
            customer.LastName = customerVM.LastName;
            customer.DateOfBirth = customerVM.DateOfBirth;

            _logger.LogInformation(InformationMessages.CustomerUpdatedSuccessfully, customer.Id, JsonConvert.SerializeObject(customer));
            return await _context.SaveChangesAsync();            
        }

        
        //Get the details of a particular customer    
        public async Task<Customer> GetCustomerData(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(b => b.Id == id);
            return customer;
        }
        //To Delete the record of a particular customer    
        public async Task<int> DeleteCustomer(Customer customer)
        {                      
            _context.Customers.Remove(customer);
            _logger.LogInformation(InformationMessages.CustomerDeletedSuccessfully, customer.Id);
            return await _context.SaveChangesAsync();
        }
    }
}
