using LibraryManagement.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Contracts
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);

        void UpdateCustomer(Customer customer);
        IEnumerable<Customer> SearchCustomers(string name);
        Customer GetById(int id);
        IEnumerable<Customer> GetAllCustomers();
        void DeleteCustomer(int id);
    }
}
