using System.Collections.Generic;
using System.Threading.Tasks;
using Alinta.Core.Models;

namespace Alinta.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> AddCustomer(CustomerViewModel customer);
        Task<int> DeleteCustomer(Customer customer);
        Task<List<Customer>> GetAllCustomers(string searchString);
        Task<Customer> GetCustomerData(int id);
        Task<int> UpdateCustomer(int id, CustomerViewModel customer);
    }
}