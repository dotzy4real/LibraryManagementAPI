using LibraryManagement.Data.DataAccess;
using LibraryManagement.Data.Entity;
using LibraryManagement.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Infrastructure
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customer;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customer = unitOfWork.Repository<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            _customer.Insert(customer);
            _unitOfWork.Commit();
        }

        public void UpdateCustomer(Customer customer)
        {
            _customer.Update(customer);
            _unitOfWork.Commit();
        }

        public IEnumerable<Customer> SearchCustomers(string name)
        {
            var customers = _customer.Get(null, x => x.OrderBy(y => y.Id)).Where(y => y.FirstName.ToLower().Contains(name.ToLower()) || y.LastName.ToLower().Contains(name.ToLower()));
            return customers!;
        }

        public Customer GetById(int id)
        {
            var customer = _customer.GetById(id);
            return customer;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = _customer.Get(null, x => x.OrderBy(y => y.Id));
            return customers;
        }

        public void DeleteCustomer(int id)
        {
            _customer.Delete(id);
            _unitOfWork.Commit();
        }
    }
}
